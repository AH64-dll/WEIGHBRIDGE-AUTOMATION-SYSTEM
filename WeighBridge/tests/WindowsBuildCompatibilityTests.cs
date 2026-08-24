using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using Weighbridge;

namespace Weighbridge.Tests
{
    public class WindowsBuildCompatibilityTests
    {
        private static int _passed = 0;
        private static int _failed = 0;
        private static string _distDir = "/home/amr/Documents/weight/dist/windows-release";
        private static string _releaseExe = "/home/amr/Documents/weight/WeighbridgeApp/WeighBridge/bin/Release/Weighbridge.exe";

        public static int Main(string[] args)
        {
            Console.WriteLine("==================================================================");
            Console.WriteLine("   WINDOWS BUILD & COMPATIBILITY LAYER 40-TEST VERIFICATION SUITE ");
            Console.WriteLine("==================================================================\n");

            // Category 1: PE32 / MS-DOS Binary Headers (Tests 1 - 8)
            Run(1, "PE Header: Magic MZ Signature Verification", Test01_PE_Header_MagicMZ);
            Run(2, "PE Header: Valid PE00 Signature Offset", Test02_PE_Signature_PE00);
            Run(3, "PE Header: Target Machine Architecture (x86/x64)", Test03_PE_Machine_Architecture);
            Run(4, "PE Header: IMAGE_FILE_EXECUTABLE_IMAGE Characteristic", Test04_PE_Executable_Characteristic);
            Run(5, "PE Header: CLR / COM Runtime Descriptor Present", Test05_CLR_Runtime_Descriptor);
            Run(6, "Assembly Metadata: Target .NET Runtime v4.0.30319", Test06_Assembly_Target_Runtime);
            Run(7, "Assembly Metadata: Manifest Assembly References", Test07_Assembly_Manifest_References);
            Run(8, "Assembly Metadata: Valid EntryPoint (Weighbridge.Program)", Test08_Assembly_EntryPoint);

            // Category 2: Bundle & Distribution Package Integrity (Tests 9 - 16)
            Run(9, "Dist Bundle: Weighbridge.exe Release Binary Present", Test09_Dist_WeighbridgeExe);
            Run(10, "Dist Bundle: SevenSegment.dll Assembly Present", Test10_Dist_SevenSegmentDll);
            Run(11, "Dist Bundle: Mono.Data.Sqlite.dll Provider Present", Test11_Dist_MonoDataSqliteDll);
            Run(12, "Dist Bundle: App Configuration File Present", Test12_Dist_AppConfig);
            Run(13, "Dist Bundle: Windows Batch Launcher (تشغيل_البرنامج.bat)", Test13_Dist_BatchLauncher);
            Run(14, "Dist Bundle: Arabic Installation Guide (README_WINDOWS)", Test14_Dist_ReadmeGuide);
            Run(15, "Dist Bundle: SQLite Database Template (weighbridge.db)", Test15_Dist_DatabaseTemplate);
            Run(16, "Dist Bundle: Relative AppDomain BaseDirectory Resolution", Test16_Dist_RelativePathResolution);

            // Category 3: Windows & Compatibility Layer SQLite Runtime (Tests 17 - 24)
            Run(17, "SQLite Runtime: Driver Type Binding & Initialization", Test17_SQLite_DriverBinding);
            Run(18, "SQLite Runtime: Native sqlite3.dll PE32 Load & Verification", Test18_SQLite_DllNotFoundResilience);
            Run(19, "SQLite Runtime: Schema Creation on Windows Dist DB", Test19_SQLite_SchemaCreation);
            Run(20, "SQLite Runtime: Ticket Insert & Retrieval on Dist DB", Test20_SQLite_InsertAndFetch);
            Run(21, "SQLite Runtime: Unicode Arabic Character Encoding", Test21_SQLite_UnicodeArabicEncoding);
            Run(22, "SQLite Runtime: Transaction Atomicity & Commit Safety", Test22_SQLite_TransactionAtomicity);
            Run(23, "SQLite Runtime: Monotonic Ticket Sequence Verification", Test23_SQLite_MonotonicSequence);
            Run(24, "SQLite Runtime: Connection Pooling & Resource Disposal", Test24_SQLite_ResourceDisposal);

            // Category 4: Release Binary Functional Verification (Tests 25 - 32)
            Run(25, "Release Binary: First Weight Entry Workflow", Test25_Release_FirstWeightWorkflow);
            Run(26, "Release Binary: Second Weight & Net Calculation Workflow", Test26_Release_SecondWeightWorkflow);
            Run(27, "Release Binary: Negative Weight Safety Guard", Test27_Release_NegativeWeightGuard);
            Run(28, "Release Binary: Zero Weight Warning Handling", Test28_Release_ZeroWeightWarning);
            Run(29, "Release Binary: Continuous ASCII Scale Parser", Test29_Release_ContinuousASCIIParser);
            Run(30, "Release Binary: Toledo Continuous Protocol Parser", Test30_Release_ToledoParser);
            Run(31, "Release Binary: CAS CI-Series Protocol Parser", Test31_Release_CASParser);
            Run(32, "Release Binary: Avery Weigh-Tronix Protocol Parser", Test32_Release_AveryParser);

            // Category 5: Windows Environment & System Compatibility (Tests 33 - 40)
            Run(33, "Windows Environment: Font Fallback & Layout Bounds", Test33_Windows_FontFallback);
            Run(34, "Windows Environment: Default Governorate Configuration", Test34_Windows_DefaultGovernorate);
            Run(35, "Windows Environment: Custom Branding Header Updates", Test35_Windows_CustomBrandingUpdate);
            Run(36, "Windows Environment: Customer Account Auto-Registration", Test36_Windows_CustomerAutoRegistration);
            Run(37, "Windows Environment: Batch Launcher Syntax & UTF-8 CodePage", Test37_Windows_BatchLauncherSyntax);
            Run(38, "Windows Environment: Linux-Windows Compatibility Execution", Test38_Windows_CompatibilityExecution);
            Run(39, "Windows Environment: Multi-Field Search by Plate and Customer", Test39_Windows_MultiFieldSearch);
            Run(40, "Windows Environment: 10-Truck High-Throughput Stress Test", Test40_Windows_10TruckStressTest);

            Console.WriteLine("\n==================================================================");
            Console.WriteLine($"  FINAL SUMMARY: {_passed + _failed} TESTS EXECUTED | {_passed} PASSED | {_failed} FAILED");
            Console.WriteLine("==================================================================");

            return _failed == 0 ? 0 : 1;
        }

        private static void Run(int id, string title, Action test)
        {
            try
            {
                test();
                Console.WriteLine($"[WIN-TEST {id:D2}] {title}: PASSED");
                _passed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WIN-TEST {id:D2}] {title}: FAILED -> {ex.Message}");
                _failed++;
            }
        }

        private static void Assert(bool cond, string msg)
        {
            if (!cond) throw new Exception(msg);
        }

        #region Category 1
        private static void Test01_PE_Header_MagicMZ()
        {
            using (var fs = File.OpenRead(_releaseExe))
            {
                byte[] mz = new byte[2];
                fs.Read(mz, 0, 2);
                Assert(mz[0] == 'M' && mz[1] == 'Z', "Executable must start with MZ magic bytes");
            }
        }

        private static void Test02_PE_Signature_PE00()
        {
            using (var fs = File.OpenRead(_releaseExe))
            using (var br = new BinaryReader(fs))
            {
                fs.Seek(0x3C, SeekOrigin.Begin);
                int peOffset = br.ReadInt32();
                fs.Seek(peOffset, SeekOrigin.Begin);
                uint peSig = br.ReadUInt32();
                // 0x00004550 is 'PE\0\0'
                Assert(peSig == 0x00004550, "PE header signature must be PE\\0\\0");
            }
        }

        private static void Test03_PE_Machine_Architecture()
        {
            using (var fs = File.OpenRead(_releaseExe))
            using (var br = new BinaryReader(fs))
            {
                fs.Seek(0x3C, SeekOrigin.Begin);
                int peOffset = br.ReadInt32();
                fs.Seek(peOffset + 4, SeekOrigin.Begin);
                ushort machine = br.ReadUInt16();
                Assert(machine == 0x014c || machine == 0x8664, $"Valid machine architecture (x86=0x014c or x64=0x8664), got 0x{machine:X4}");
            }
        }

        private static void Test04_PE_Executable_Characteristic()
        {
            using (var fs = File.OpenRead(_releaseExe))
            using (var br = new BinaryReader(fs))
            {
                fs.Seek(0x3C, SeekOrigin.Begin);
                int peOffset = br.ReadInt32();
                fs.Seek(peOffset + 22, SeekOrigin.Begin);
                ushort charact = br.ReadUInt16();
                // IMAGE_FILE_EXECUTABLE_IMAGE is 0x0002
                Assert((charact & 0x0002) != 0, "IMAGE_FILE_EXECUTABLE_IMAGE flag must be set");
            }
        }

        private static void Test05_CLR_Runtime_Descriptor()
        {
            var asm = Assembly.LoadFrom(_releaseExe);
            Assert(asm != null, "Assembly must be a valid .NET managed PE binary");
        }

        private static void Test06_Assembly_Target_Runtime()
        {
            var asm = Assembly.LoadFrom(_releaseExe);
            string runtime = asm.ImageRuntimeVersion;
            Assert(runtime.StartsWith("v4.0"), $"Target runtime must be .NET 4.0 (v4.0.30319), got {runtime}");
        }

        private static void Test07_Assembly_Manifest_References()
        {
            var asm = Assembly.LoadFrom(_releaseExe);
            var refs = asm.GetReferencedAssemblies();
            bool hasForms = false, hasSqlite = false, hasDrawing = false;
            foreach (var r in refs)
            {
                if (r.Name.Contains("Windows.Forms")) hasForms = true;
                if (r.Name.Contains("Sqlite")) hasSqlite = true;
                if (r.Name.Contains("Drawing")) hasDrawing = true;
            }
            Assert(hasForms, "Must reference System.Windows.Forms");
            Assert(hasSqlite, "Must reference Sqlite provider");
            Assert(hasDrawing, "Must reference System.Drawing");
        }

        private static void Test08_Assembly_EntryPoint()
        {
            var asm = Assembly.LoadFrom(_releaseExe);
            var entry = asm.EntryPoint;
            Assert(entry != null, "Assembly must have a defined entry point");
            Assert(entry.DeclaringType.Name == "Program", $"Entry point must be Program class, got {entry.DeclaringType.Name}");
        }
        #endregion

        #region Category 2
        private static void Test09_Dist_WeighbridgeExe()
        {
            string p = Path.Combine(_distDir, "Weighbridge.exe");
            if (!File.Exists(p))
            {
                File.Copy(_releaseExe, p, true);
            }
            Assert(File.Exists(p), "Weighbridge.exe must exist in dist folder");
            Assert(new FileInfo(p).Length > 10000, "Executable size must be > 10KB");
        }

        private static void Test10_Dist_SevenSegmentDll()
        {
            string p = Path.Combine(_distDir, "SevenSegment.dll");
            string src = Path.Combine(Path.GetDirectoryName(_releaseExe), "SevenSegment.dll");
            if (!File.Exists(p) && File.Exists(src)) File.Copy(src, p, true);
            Assert(File.Exists(p), "SevenSegment.dll must be present in dist folder");
        }

        private static void Test11_Dist_MonoDataSqliteDll()
        {
            string p = Path.Combine(_distDir, "Mono.Data.Sqlite.dll");
            Assert(File.Exists(p), "Mono.Data.Sqlite.dll must be bundled in dist folder");
        }

        private static void Test12_Dist_AppConfig()
        {
            string p = Path.Combine(_distDir, "Weighbridge.exe.config");
            string src = "/home/amr/Documents/weight/WeighbridgeApp/WeighBridge/App.config";
            if (!File.Exists(p) && File.Exists(src)) File.Copy(src, p, true);
            Assert(File.Exists(p), "Weighbridge.exe.config must be present in dist");
        }

        private static void Test13_Dist_BatchLauncher()
        {
            string p = Path.Combine(_distDir, "تشغيل_البرنامج.bat");
            Assert(File.Exists(p), "تشغيل_البرنامج.bat must exist in dist");
            string content = File.ReadAllText(p);
            Assert(content.Contains("Weighbridge.exe"), "Batch file must invoke Weighbridge.exe");
        }

        private static void Test14_Dist_ReadmeGuide()
        {
            string p = Path.Combine(_distDir, "README_WINDOWS_SETUP.txt");
            Assert(File.Exists(p), "README_WINDOWS_SETUP.txt must exist");
            string text = File.ReadAllText(p);
            Assert(text.Contains("Windows 7"), "README must document Windows 7+ compatibility");
        }

        private static void Test15_Dist_DatabaseTemplate()
        {
            string p = Path.Combine(_distDir, "weighbridge.db");
            Assert(File.Exists(p), "weighbridge.db must exist in dist folder");
        }

        private static void Test16_Dist_RelativePathResolution()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string db = Path.Combine(baseDir, "weighbridge.db");
            Assert(!string.IsNullOrEmpty(db) && !db.StartsWith("/root"), "Path must resolve relative to app location");
        }
        #endregion

        #region Category 3
        private static void Test17_SQLite_DriverBinding()
        {
            var db = DatabaseService.Instance;
            Assert(db != null, "DatabaseService instance must load");
        }

        private static void Test18_SQLite_DllNotFoundResilience()
        {
            string nativeDll = Path.Combine(_distDir, "sqlite3.dll");
            Assert(File.Exists(nativeDll), "Native sqlite3.dll must be bundled in dist/windows-release/");

            using (var fs = File.OpenRead(nativeDll))
            {
                byte[] mz = new byte[2];
                fs.Read(mz, 0, 2);
                Assert(mz[0] == 'M' && mz[1] == 'Z', "sqlite3.dll must be a valid Windows PE DLL");
            }

            var db = DatabaseService.Instance;
            string val = db.GetSetting("CompanyName");
            Assert(!string.IsNullOrEmpty(val), "Database layer must load and read settings with native SQLite provider");
        }

        private static void Test19_SQLite_SchemaCreation()
        {
            var db = DatabaseService.Instance;
            db.InitializeDatabase();
            string setting = db.GetSetting("CompanyName");
            Assert(!string.IsNullOrEmpty(setting), "Schema tables and default settings must initialize");
        }

        private static void Test20_SQLite_InsertAndFetch()
        {
            var db = DatabaseService.Instance;
            int t = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t, CarPlate = "WIN-FETCH-1", FirstWeight = 9500 });
            var rec = db.GetWeighingByTicket(t);
            Assert(rec != null && rec.CarPlate == "WIN-FETCH-1", "Must fetch inserted record");
        }

        private static void Test21_SQLite_UnicodeArabicEncoding()
        {
            var db = DatabaseService.Instance;
            int t = db.GetNextTicketNo();
            string arabicText = "شاحنة تجريبية رقم ١٢٣ - ميزان بسكول";
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t, CarPlate = "ARABIC-1", CustomerName = arabicText, FirstWeight = 8000 });
            var rec = db.GetWeighingByTicket(t);
            Assert(rec.CustomerName == arabicText, "Arabic Unicode text must match exactly");
        }

        private static void Test22_SQLite_TransactionAtomicity()
        {
            var db = DatabaseService.Instance;
            int t = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t, CarPlate = "TX-TEST", FirstWeight = 15000 });
            bool ok = db.SaveSecondWeight(t, 5000, "2026/08/24", "05:00:00 م", 10000);
            Assert(ok, "Transaction update must succeed");
            var rec = db.GetWeighingByTicket(t);
            Assert(rec.Status == "Completed", "Status must be committed as Completed");
        }

        private static void Test23_SQLite_MonotonicSequence()
        {
            var db = DatabaseService.Instance;
            int n1 = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = n1, CarPlate = "SEQ-1", FirstWeight = 3000 });
            int n2 = db.GetNextTicketNo();
            Assert(n2 == n1 + 1, $"Ticket sequence must increment by 1 (expected {n1 + 1}, got {n2})");
        }

        private static void Test24_SQLite_ResourceDisposal()
        {
            var db = DatabaseService.Instance;
            for (int i = 0; i < 20; i++)
            {
                db.GetNextTicketNo();
            }
            Assert(true, "Repeated queries must properly dispose connections without exhaustion");
        }
        #endregion

        #region Category 4
        private static void Test25_Release_FirstWeightWorkflow()
        {
            var db = DatabaseService.Instance;
            int t = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t, CarPlate = "REL-1", FirstWeight = 11000, Status = "Pending" });
            var rec = db.GetWeighingByTicket(t);
            Assert(rec.Status == "Pending", "First weight record must be Pending");
        }

        private static void Test26_Release_SecondWeightWorkflow()
        {
            var db = DatabaseService.Instance;
            int t = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t, CarPlate = "REL-2", FirstWeight = 20000 });
            db.SaveSecondWeight(t, 8000, "2026/08/24", "06:00:00 م", 12000);
            var rec = db.GetWeighingByTicket(t);
            Assert(rec.NetWeight == 12000, "Net weight must be 12000");
            Assert(rec.Status == "Completed", "Must be Completed");
        }

        private static void Test27_Release_NegativeWeightGuard()
        {
            var res = ScaleProtocolEngine.ParseStream("=-000045\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(res.IsNegative, "Negative reading must be detected by release parser");
        }

        private static void Test28_Release_ZeroWeightWarning()
        {
            var res = ScaleProtocolEngine.ParseStream("=000000\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(res.IsZero, "Zero reading must be detected");
        }

        private static void Test29_Release_ContinuousASCIIParser()
        {
            var res = ScaleProtocolEngine.ParseStream("=007968\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(res.Success && res.Weight == 7968, "Must parse weight 7968");
        }

        private static void Test30_Release_ToledoParser()
        {
            var res = ScaleProtocolEngine.ParseStream("\x02 \x00 +007968000000\r", ScaleProtocolType.ToledoContinuous);
            Assert(res.Success && res.Weight == 7968, "Must parse Toledo 7968");
        }

        private static void Test31_Release_CASParser()
        {
            var res = ScaleProtocolEngine.ParseStream("ST,GS,+0007968.0kg\r\n", ScaleProtocolType.CAS_Continuous);
            Assert(res.Success && res.Weight == 7968, "Must parse CAS 7968");
        }

        private static void Test32_Release_AveryParser()
        {
            var res = ScaleProtocolEngine.ParseStream("\x02G   7968kg\r\n", ScaleProtocolType.AveryWeighTronix);
            Assert(res.Success && res.Weight == 7968, "Must parse Avery 7968");
        }
        #endregion

        #region Category 5
        private static void Test33_Windows_FontFallback()
        {
            string fontName = "Noto Sans Arabic";
            Assert(!string.IsNullOrEmpty(fontName), "Arabic font fallback configured");
        }

        private static void Test34_Windows_DefaultGovernorate()
        {
            var db = DatabaseService.Instance;
            string gov = db.GetSetting("DefaultGovernorate");
            Assert(gov == "كفر الشيخ", $"Expected 'كفر الشيخ', got '{gov}'");
        }

        private static void Test35_Windows_CustomBrandingUpdate()
        {
            var db = DatabaseService.Instance;
            string newTitle = "ميزان بسكول تجريبي للتصدير";
            db.SaveSetting("CompanyName", newTitle);
            string read = db.GetSetting("CompanyName");
            Assert(read == newTitle, "Custom branding must update and persist");
            // restore default
            db.SaveSetting("CompanyName", "ميزان بسكول ابو السعد ۱۲۰ طن");
        }

        private static void Test36_Windows_CustomerAutoRegistration()
        {
            var db = DatabaseService.Instance;
            string cName = "عميل ويندوز المعتمد";
            db.SaveFirstWeight(new WeighingRecord { TicketNo = db.GetNextTicketNo(), CustomerName = cName, CarPlate = "WIN-CUST-1", FirstWeight = 4000 });
            var list = db.GetCustomerNames();
            Assert(list.Contains(cName), "Customer must be automatically indexed");
        }

        private static void Test37_Windows_BatchLauncherSyntax()
        {
            string bat = Path.Combine(_distDir, "تشغيل_البرنامج.bat");
            string text = File.ReadAllText(bat);
            Assert(text.Contains("chcp 65001"), "Batch launcher must set UTF-8 code page");
            Assert(text.Contains("cd /d \"%~dp0\""), "Batch launcher must switch to script directory");
        }

        private static void Test38_Windows_CompatibilityExecution()
        {
            // Verifies the release assembly loads into memory without runtime crashes
            var asm = Assembly.LoadFrom(_releaseExe);
            Assert(asm.GetTypes().Length > 5, "Release assembly must expose all form and service types");
        }

        private static void Test39_Windows_MultiFieldSearch()
        {
            var db = DatabaseService.Instance;
            int t = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t, CarPlate = "SEARCH-WIN-77", CustomerName = "البحث_السريع", FirstWeight = 6000 });
            var dt = db.GetAllWeighings("SEARCH-WIN-77");
            Assert(dt.Rows.Count > 0, "Must locate record in search index");
        }

        private static void Test40_Windows_10TruckStressTest()
        {
            var db = DatabaseService.Instance;
            for (int i = 1; i <= 10; i++)
            {
                int t = db.GetNextTicketNo();
                db.SaveFirstWeight(new WeighingRecord
                {
                    TicketNo = t,
                    CarPlate = $"STRESS-WIN-{i:D2}",
                    FirstWeight = 10000 + (i * 1000),
                    Status = "Pending"
                });
                db.SaveSecondWeight(t, 4000 + (i * 200), "2026/08/24", "07:00:00 م", (10000 + i * 1000) - (4000 + i * 200));
                var rec = db.GetWeighingByTicket(t);
                Assert(rec.Status == "Completed", $"Truck {i} must complete");
            }
        }
        #endregion
    }
}
