using System;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Weighbridge
{
    public enum ScaleProtocolType
    {
        ContinuousASCII,
        ToledoContinuous,
        CAS_Continuous,
        AveryWeighTronix,
        CustomRegex
    }

    public struct ScaleReadingResult
    {
        public bool Success;
        public int Weight;
        public bool IsStable;
        public bool IsNegative;
        public bool IsZero;
        public string ErrorMessage;
        public string RawData;
    }

    public class ScaleProtocolEngine
    {
        private static ScaleProtocolEngine _instance;
        public static ScaleProtocolEngine Instance => _instance ?? (_instance = new ScaleProtocolEngine());

        private SerialPort _serialPort;
        private Thread _simulatorThread;
        private volatile bool _isSimulating;
        private readonly object _lockObj = new object();

        public ScaleProtocolType Protocol { get; set; } = ScaleProtocolType.ContinuousASCII;
        public bool IsSimulatorEnabled { get; set; } = false;
        public bool IsConnected => (_serialPort != null && _serialPort.IsOpen) || _isSimulating;

        public int CurrentWeight { get; private set; } = 0;
        public bool IsStable { get; private set; } = true;
        public bool IsNegative => CurrentWeight < 0;
        public bool IsZero => CurrentWeight == 0;
        public bool HasValidReading { get; private set; } = true;

        public int RxEventInterval { get; set; } = 12;
        public int InputLen { get; set; } = 0;

        public event Action<int, bool, bool> OnWeightChanged; // weight, isStable, isNegative
        public event Action<string> OnRawData;
        public event Action<string> OnStatusChanged;
        public event Action<string> OnError;

        private StringBuilder _rxBuffer = new StringBuilder();

        public ScaleProtocolEngine()
        {
        }

        public void Start()
        {
            var db = DatabaseService.Instance;
            string portName = db.GetSetting("PortName", "COM1");
            int baudRate = int.TryParse(db.GetSetting("BaudRate", "9600"), out int b) ? b : 9600;
            int dataBits = int.TryParse(db.GetSetting("DataBits", "8"), out int dbits) ? dbits : 8;
            Parity parity = Enum.TryParse(db.GetSetting("Parity", "None"), true, out Parity p) ? p : Parity.None;
            StopBits stopBits = Enum.TryParse(db.GetSetting("StopBits", "One"), true, out StopBits sb) ? sb : StopBits.One;
            Handshake handshake = Enum.TryParse(db.GetSetting("Handshake", "None"), true, out Handshake hs) ? hs : Handshake.None;
            
            string protoStr = db.GetSetting("ScaleProtocol", "ContinuousASCII");
            Protocol = Enum.TryParse(protoStr, true, out ScaleProtocolType pt) ? pt : ScaleProtocolType.ContinuousASCII;
            
            bool sim = db.GetSetting("SimulatorEnabled", "False").Equals("True", StringComparison.OrdinalIgnoreCase);
            IsSimulatorEnabled = sim;

            if (IsSimulatorEnabled)
            {
                StartSimulator();
                return;
            }

            try
            {
                StopSimulator();

                if (_serialPort != null)
                {
                    if (_serialPort.IsOpen) _serialPort.Close();
                    _serialPort.Dispose();
                }

                _serialPort = new SerialPort
                {
                    PortName = portName,
                    BaudRate = baudRate,
                    DataBits = dataBits,
                    Parity = parity,
                    StopBits = stopBits,
                    Handshake = handshake,
                    ReadTimeout = 500,
                    WriteTimeout = 500
                };

                _serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.Open();

                OnStatusChanged?.Invoke($"متصل بالمنفذ {portName} ({baudRate} bps)");
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"تعذر فتح المنفذ التسلسلي {portName}: {ex.Message}");
                StartSimulator();
            }
        }

        public void Stop()
        {
            StopSimulator();

            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Close();
                    _serialPort.Dispose();
                    _serialPort = null;
                }
            }
            catch { }

            OnStatusChanged?.Invoke("الميزان غير متصل");
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPort == null || !_serialPort.IsOpen) return;

            try
            {
                int bytesToRead = _serialPort.BytesToRead;
                if (bytesToRead <= 0) return;

                byte[] buffer = new byte[bytesToRead];
                int read = _serialPort.Read(buffer, 0, bytesToRead);
                string text = Encoding.ASCII.GetString(buffer, 0, read);

                OnRawData?.Invoke(text);

                lock (_lockObj)
                {
                    _rxBuffer.Append(text);
                    if (_rxBuffer.Length > 2048)
                    {
                        _rxBuffer.Remove(0, _rxBuffer.Length - 512);
                    }

                    ProcessBuffer();
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        public static ScaleReadingResult ParseStream(string content, ScaleProtocolType protocol)
        {
            var res = new ScaleReadingResult { Success = false, IsStable = true };
            if (string.IsNullOrEmpty(content)) return res;

            try
            {
                switch (protocol)
                {
                    case ScaleProtocolType.ContinuousASCII:
                        // Matches +004354, -000015, =004354, =-00015, or plain 4354kg, -15kg
                        var m = Regex.Match(content, @"([+=-])\s*(-?\d{1,7})", RegexOptions.RightToLeft);
                        if (m.Success)
                        {
                            string sign = m.Groups[1].Value;
                            string valStr = m.Groups[2].Value;
                            if (int.TryParse(valStr, out int val))
                            {
                                if (sign == "-" && val > 0) val = -val;
                                res.Weight = val;
                                res.IsNegative = val < 0;
                                res.IsZero = val == 0;
                                res.IsStable = true;
                                res.Success = true;
                            }
                        }
                        else
                        {
                            var mPlain = Regex.Match(content, @"(-?\d{1,7})\s*(?:kg|كجم|\r|\n)", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
                            if (mPlain.Success && int.TryParse(mPlain.Groups[1].Value, out int val2))
                            {
                                res.Weight = val2;
                                res.IsNegative = val2 < 0;
                                res.IsZero = val2 == 0;
                                res.IsStable = true;
                                res.Success = true;
                            }
                        }
                        break;

                    case ScaleProtocolType.ToledoContinuous:
                        // Toledo continuous: \x02<StatusA><StatusB><StatusC><Weight 6 digits><Tare 6 digits>\r
                        var mToledo = Regex.Match(content, @"\x02(.)(.)(.)([+-]?\d{5,6})", RegexOptions.RightToLeft);
                        if (mToledo.Success)
                        {
                            char statusB = mToledo.Groups[2].Value[0];
                            bool isNegative = (statusB & 0x01) != 0;
                            bool isMotion = (statusB & 0x02) != 0;

                            if (int.TryParse(mToledo.Groups[4].Value, out int valT))
                            {
                                if (isNegative && valT > 0) valT = -valT;
                                res.Weight = valT;
                                res.IsNegative = valT < 0 || isNegative;
                                res.IsZero = valT == 0;
                                res.IsStable = !isMotion;
                                res.Success = true;
                            }
                        }
                        break;

                    case ScaleProtocolType.CAS_Continuous:
                        // CAS format: ST,GS,+0004354.0kg or US,GS,-0000015.0kg
                        var mCas = Regex.Match(content, @"(ST|US),\s*GS,\s*([+-]?\s*\d+)", RegexOptions.RightToLeft);
                        if (mCas.Success)
                        {
                            res.IsStable = mCas.Groups[1].Value == "ST";
                            string digits = mCas.Groups[2].Value.Replace(" ", "");
                            if (int.TryParse(digits, out int valC))
                            {
                                res.Weight = valC;
                                res.IsNegative = valC < 0;
                                res.IsZero = valC == 0;
                                res.Success = true;
                            }
                        }
                        break;

                    case ScaleProtocolType.AveryWeighTronix:
                        var mAvery = Regex.Match(content, @"\x02G\s*([+-]?\d+)", RegexOptions.RightToLeft);
                        if (mAvery.Success && int.TryParse(mAvery.Groups[1].Value, out int valA))
                        {
                            res.Weight = valA;
                            res.IsNegative = valA < 0;
                            res.IsZero = valA == 0;
                            res.IsStable = true;
                            res.Success = true;
                        }
                        break;

                    default:
                        var mGen = Regex.Match(content, @"([+-]?\d{1,7})", RegexOptions.RightToLeft);
                        if (mGen.Success && int.TryParse(mGen.Groups[1].Value, out int valG))
                        {
                            res.Weight = valG;
                            res.IsNegative = valG < 0;
                            res.IsZero = valG == 0;
                            res.IsStable = true;
                            res.Success = true;
                        }
                        break;
                }

                // Bounds safety check: Truck scales typically range from -5,000 kg to 200,000 kg
                if (res.Success && (res.Weight < -5000 || res.Weight > 200000))
                {
                    res.Success = false;
                    res.ErrorMessage = "قراءة خارج النطاق الطبيعي لميزان الشاحنات";
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.ErrorMessage = ex.Message;
            }

            return res;
        }

        private void ProcessBuffer()
        {
            string content = _rxBuffer.ToString();
            var res = ParseStream(content, Protocol);

            if (res.Success)
            {
                CurrentWeight = res.Weight;
                IsStable = res.IsStable;
                HasValidReading = true;
                OnWeightChanged?.Invoke(CurrentWeight, IsStable, IsNegative);
            }
        }

        #region Virtual Hardware Simulator
        public void StartSimulator()
        {
            StopSimulator();
            _isSimulating = true;
            _simulatorThread = new Thread(SimulatorLoop)
            {
                IsBackground = true,
                Name = "ScaleSimulatorThread"
            };
            _simulatorThread.Start();
            OnStatusChanged?.Invoke("محاكي الميزان الافتراضي (قيد التشغيل)");
        }

        public void StopSimulator()
        {
            _isSimulating = false;
            if (_simulatorThread != null && _simulatorThread.IsAlive)
            {
                _simulatorThread.Join(200);
                _simulatorThread = null;
            }
        }

        public void SetSimulatedWeight(int weight, bool stable = true)
        {
            CurrentWeight = weight;
            IsStable = stable;
            HasValidReading = true;
            OnWeightChanged?.Invoke(CurrentWeight, IsStable, IsNegative);
        }

        private void SimulatorLoop()
        {
            var rand = new Random();
            int baseWeight = 4354;

            while (_isSimulating)
            {
                try
                {
                    int jitter = rand.Next(-2, 3);
                    CurrentWeight = Math.Max(0, baseWeight + jitter);
                    IsStable = true;

                    OnWeightChanged?.Invoke(CurrentWeight, IsStable, IsNegative);
                    OnRawData?.Invoke($"={CurrentWeight:D6}\r\n");

                    Thread.Sleep(350);
                }
                catch { break; }
            }
        }
        #endregion
    }
}
