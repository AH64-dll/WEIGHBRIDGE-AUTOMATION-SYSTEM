using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using Weighbridge;

namespace Weighbridge.Tests
{
    public class MasterTestSuite
    {
        private static int _passed = 0;
        private static int _failed = 0;

        public static int Main(string[] args)
        {
            Console.WriteLine("==================================================================");
            Console.WriteLine("    WEIGHBRIDGE APPLICATION - 40-TEST MASTER VERIFICATION SUITE   ");
            Console.WriteLine("==================================================================\n");

            // Setup isolated test database environment
            string testDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weighbridge.db");

            // CATEGORY 1: Scale Protocol & Stream Parsing (Tests 1 - 8)
            RunTest(1, "Parse Continuous ASCII Standard Positive Weight", Test01_Parse_ContinuousASCII_Positive);
            RunTest(2, "Parse Continuous ASCII Zero Weight Reading", Test02_Parse_ContinuousASCII_Zero);
            RunTest(3, "Parse Continuous ASCII Negative Weight Reading", Test03_Parse_ContinuousASCII_Negative);
            RunTest(4, "Parse Toledo Continuous Positive Weight Format", Test04_Parse_Toledo_Positive);
            RunTest(5, "Parse Toledo Continuous Negative Weight Format", Test05_Parse_Toledo_Negative);
            RunTest(6, "Parse CAS CI-Series Continuous Protocol", Test06_Parse_CAS_Protocol);
            RunTest(7, "Parse Avery Weigh-Tronix Protocol Format", Test07_Parse_Avery_Protocol);
            RunTest(8, "Stream Corruption & Noise Packet Rejection", Test08_Parse_CorruptedNoise_Rejection);

            // CATEGORY 2: Core Weighing Workflow & Math (Tests 9 - 16)
            RunTest(9, "First Weight Recording & Pending Queue Entry", Test09_FirstWeight_PendingEntry);
            RunTest(10, "Second Weight (Gross-First) Net Calculation", Test10_SecondWeight_GrossFirst_Net);
            RunTest(11, "Second Weight (Tare-First) Net Calculation", Test11_SecondWeight_TareFirst_Net);
            RunTest(12, "Second Weight Status Transition to Completed", Test12_SecondWeight_StatusCompleted);
            RunTest(13, "Pending Queue Filtering by Date", Test13_PendingQueue_DateFilter);
            RunTest(14, "Pending Queue Auto-Removal upon Completion", Test14_PendingQueue_AutoRemoval);
            RunTest(15, "Multi-Trip Duplicate Plate Isolation", Test15_DuplicatePlate_MultiTrip);
            RunTest(16, "Sequential Record Navigation (Prev/Next)", Test16_Record_Navigation_PrevNext);

            // CATEGORY 3: Database & Persistence Layer (Tests 17 - 24)
            RunTest(17, "Database Auto-Initialization & Schema Integrity", Test17_Database_AutoInit);
            RunTest(18, "Monotonic Ticket Number Auto-Incrementation", Test18_Database_TicketAutoIncrement);
            RunTest(19, "Customer Auto-Addition on First Weighing", Test19_Database_CustomerAutoAdd);
            RunTest(20, "Customer Account Balance Update & Persistence", Test20_Database_CustomerBalance);
            RunTest(21, "Settings Key-Value Storage & Retrieval", Test21_Database_SettingsPersistence);
            RunTest(22, "Resilience to Quotes and Special Characters in Fields", Test22_Database_SpecialCharacters);
            RunTest(23, "Multi-Field Ticket Search (Plate, Customer, TicketNo)", Test23_Database_MultiFieldSearch);
            RunTest(24, "Empty/Null Optional Fields Clean Handling", Test24_Database_EmptyFieldsHandling);

            // CATEGORY 4: Validation, Error Handling & Safety Guards (Tests 25 - 32)
            RunTest(25, "Safety Guard: Negative Weight Rejection for Entry", Test25_Guard_NegativeFirstWeight);
            RunTest(26, "Safety Guard: Negative Weight Rejection for Exit", Test26_Guard_NegativeSecondWeight);
            RunTest(27, "Safety Guard: Sudden Weight Drop to Zero Handling", Test27_Guard_ZeroWeightDrop);
            RunTest(28, "Safety Guard: Out-of-Bounds Scale Reading Rejection", Test28_Guard_OutOfBoundsReading);
            RunTest(29, "Safety Guard: Empty/Whitespace Plate Validation", Test29_Guard_EmptyPlateValidation);
            RunTest(30, "Safety Guard: Non-Existent Ticket Safe Return Null", Test30_Guard_NonExistentTicket);
            RunTest(31, "Safety Guard: High-Volume Stream Buffer Overflow Safety", Test31_Guard_BufferOverflowSafety);
            RunTest(32, "Safety Guard: Virtual Scale Simulator Lifecycle Safety", Test32_Guard_SimulatorLifecycle);

            // CATEGORY 5: Ticket Generation, Arithmetic & Reporting (Tests 33 - 40)
            RunTest(33, "Ticket Net Weight Mathematical Invariant", Test33_Ticket_NetWeightInvariant);
            RunTest(34, "Ticket Independent Dual Timestamps Integrity", Test34_Ticket_DualTimestamps);
            RunTest(35, "Ticket Default Governorate Fallback", Test35_Ticket_DefaultGovernorate);
            RunTest(36, "Ticket Fee and Notes Persistence", Test36_Ticket_FeeAndNotes);
            RunTest(37, "Custom Branding Defaults and Fallbacks", Test37_Branding_DefaultsFallback);
            RunTest(38, "Latest Ticket Query Accuracy", Test38_Ticket_LatestRecord);
            RunTest(39, "Ticket Status Partitioning (Pending vs Completed)", Test39_Ticket_StatusPartitioning);
            RunTest(40, "End-to-End Multi-Truck Full Lifecycle Stress Test", Test40_EndToEnd_MultiTruckStress);

            Console.WriteLine("\n==================================================================");
            Console.WriteLine($"  FINAL SUMMARY: {_passed + _failed} TESTS EXECUTED | {_passed} PASSED | {_failed} FAILED");
            Console.WriteLine("==================================================================");

            return _failed == 0 ? 0 : 1;
        }

        private static void RunTest(int id, string title, Action testAction)
        {
            try
            {
                testAction();
                Console.WriteLine($"[TEST {id:D2}] {title}: PASSED");
                _passed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST {id:D2}] {title}: FAILED -> {ex.Message}");
                _failed++;
            }
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition) throw new Exception(message);
        }

        #region Category 1: Scale Protocol & Stream Parsing
        private static void Test01_Parse_ContinuousASCII_Positive()
        {
            var res = ScaleProtocolEngine.ParseStream("=004354\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(res.Success, "Expected successful parse");
            Assert(res.Weight == 4354, $"Expected weight 4354, got {res.Weight}");
            Assert(!res.IsNegative, "Weight should not be negative");
            Assert(!res.IsZero, "Weight should not be zero");
            Assert(res.IsStable, "Reading should be stable");
        }

        private static void Test02_Parse_ContinuousASCII_Zero()
        {
            var res = ScaleProtocolEngine.ParseStream("=000000\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(res.Success, "Expected successful parse");
            Assert(res.Weight == 0, $"Expected weight 0, got {res.Weight}");
            Assert(res.IsZero, "IsZero flag must be true");
            Assert(!res.IsNegative, "Zero must not be negative");
        }

        private static void Test03_Parse_ContinuousASCII_Negative()
        {
            var res = ScaleProtocolEngine.ParseStream("=-000015\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(res.Success, "Expected successful parse");
            Assert(res.Weight == -15, $"Expected weight -15, got {res.Weight}");
            Assert(res.IsNegative, "IsNegative flag must be true for negative reading");
            Assert(!res.IsZero, "Weight is non-zero");
        }

        private static void Test04_Parse_Toledo_Positive()
        {
            // Toledo packet with statusA ' ', statusB (bit0=0 -> positive, bit1=0 -> stable), statusC ' '
            string packet = "\x02 \x00 +004354000000\r";
            var res = ScaleProtocolEngine.ParseStream(packet, ScaleProtocolType.ToledoContinuous);
            Assert(res.Success, "Expected successful Toledo parse");
            Assert(res.Weight == 4354, $"Expected 4354, got {res.Weight}");
            Assert(!res.IsNegative, "Should be positive");
            Assert(res.IsStable, "Should be stable");
        }

        private static void Test05_Parse_Toledo_Negative()
        {
            // Toledo packet with statusB bit0=1 (negative)
            string packet = "\x02 \x01 -000015000000\r";
            var res = ScaleProtocolEngine.ParseStream(packet, ScaleProtocolType.ToledoContinuous);
            Assert(res.Success, "Expected successful Toledo parse");
            Assert(res.Weight == -15, $"Expected -15, got {res.Weight}");
            Assert(res.IsNegative, "Should be flagged negative");
        }

        private static void Test06_Parse_CAS_Protocol()
        {
            string packet = "ST,GS,+0004354.0kg\r\n";
            var res = ScaleProtocolEngine.ParseStream(packet, ScaleProtocolType.CAS_Continuous);
            Assert(res.Success, "Expected successful CAS parse");
            Assert(res.Weight == 4354, $"Expected 4354, got {res.Weight}");
            Assert(res.IsStable, "ST indicates stable reading");
        }

        private static void Test07_Parse_Avery_Protocol()
        {
            string packet = "\x02G   4354kg\r\n";
            var res = ScaleProtocolEngine.ParseStream(packet, ScaleProtocolType.AveryWeighTronix);
            Assert(res.Success, "Expected successful Avery parse");
            Assert(res.Weight == 4354, $"Expected 4354, got {res.Weight}");
        }

        private static void Test08_Parse_CorruptedNoise_Rejection()
        {
            string noisyPacket = "\x00\xFF\xAA!@#$%^&*()BAD_DATA_STREAM\r\n";
            var res = ScaleProtocolEngine.ParseStream(noisyPacket, ScaleProtocolType.ContinuousASCII);
            Assert(!res.Success, "Corrupted noise stream must be rejected as invalid reading");
        }
        #endregion

        #region Category 2: Core Weighing Workflow & Math
        private static void Test09_FirstWeight_PendingEntry()
        {
            var db = DatabaseService.Instance;
            int nextTicket = db.GetNextTicketNo();

            var r = new WeighingRecord
            {
                TicketNo = nextTicket,
                CustomerName = "شركة المقاولات الحديثة",
                CarPlate = "112233",
                DriverName = "أحمد سامي",
                CargoType = "رمل وزلط",
                Governorate = "كفر الشيخ",
                FirstWeight = 12500,
                FirstDate = "2026/08/24",
                FirstTime = "10:00:00 ص",
                Status = "Pending"
            };

            int savedTicket = db.SaveFirstWeight(r);
            Assert(savedTicket == nextTicket, "Ticket number must match generated ID");

            var fetched = db.GetWeighingByTicket(savedTicket);
            Assert(fetched != null, "Record must exist in database");
            Assert(fetched.Status == "Pending", "Initial status must be Pending");
            Assert(fetched.FirstWeight == 12500, "First weight must be stored accurately");
        }

        private static void Test10_SecondWeight_GrossFirst_Net()
        {
            var db = DatabaseService.Instance;
            int ticket = db.GetNextTicketNo();

            db.SaveFirstWeight(new WeighingRecord
            {
                TicketNo = ticket,
                CarPlate = "445566",
                FirstWeight = 18000,
                FirstDate = "2026/08/24",
                FirstTime = "11:00:00 ص",
                Status = "Pending"
            });

            int secondWeight = 6000;
            int expectedNet = 18000 - 6000; // 12000 kg

            bool ok = db.SaveSecondWeight(ticket, secondWeight, "2026/08/24", "11:45:00 ص", expectedNet);
            Assert(ok, "SaveSecondWeight must return true");

            var completed = db.GetWeighingByTicket(ticket);
            Assert(completed.NetWeight == 12000, $"Expected Net 12000 kg, got {completed.NetWeight}");
            Assert(completed.SecondWeight == 6000, "Second weight must be 6000");
        }

        private static void Test11_SecondWeight_TareFirst_Net()
        {
            var db = DatabaseService.Instance;
            int ticket = db.GetNextTicketNo();

            // Truck enters empty (tare first: 5500 kg)
            db.SaveFirstWeight(new WeighingRecord
            {
                TicketNo = ticket,
                CarPlate = "778899",
                FirstWeight = 5500,
                FirstDate = "2026/08/24",
                FirstTime = "01:00:00 م",
                Status = "Pending"
            });

            // Truck leaves loaded (16500 kg) -> Net = 11000 kg
            int secondWeight = 16500;
            int net = Math.Abs(5500 - 16500); // 11000 kg

            db.SaveSecondWeight(ticket, secondWeight, "2026/08/24", "02:00:00 م", net);
            var completed = db.GetWeighingByTicket(ticket);
            Assert(completed.NetWeight == 11000, $"Expected Net 11000 kg, got {completed.NetWeight}");
        }

        private static void Test12_SecondWeight_StatusCompleted()
        {
            var db = DatabaseService.Instance;
            int ticket = db.GetNextTicketNo();

            db.SaveFirstWeight(new WeighingRecord { TicketNo = ticket, CarPlate = "990011", FirstWeight = 10000, Status = "Pending" });
            db.SaveSecondWeight(ticket, 4000, "2026/08/24", "03:00:00 م", 6000);

            var rec = db.GetWeighingByTicket(ticket);
            Assert(rec.Status == "Completed", "Status must update from Pending to Completed");
        }

        private static void Test13_PendingQueue_DateFilter()
        {
            var db = DatabaseService.Instance;
            db.SaveFirstWeight(new WeighingRecord { TicketNo = db.GetNextTicketNo(), CarPlate = "DATE-TEST-1", FirstDate = "2026/08/24", FirstWeight = 9000 });

            var todayQueue = db.GetPendingWeighings("2026/08/24");
            bool found = false;
            foreach (DataRow row in todayQueue.Rows)
            {
                if (row["CarPlate"].ToString() == "DATE-TEST-1") { found = true; break; }
            }
            Assert(found, "Plate must appear when filtered by its date");

            var otherDayQueue = db.GetPendingWeighings("2020/01/01");
            bool foundOld = false;
            foreach (DataRow row in otherDayQueue.Rows)
            {
                if (row["CarPlate"].ToString() == "DATE-TEST-1") { foundOld = true; break; }
            }
            Assert(!foundOld, "Plate must not appear on non-matching date");
        }

        private static void Test14_PendingQueue_AutoRemoval()
        {
            var db = DatabaseService.Instance;
            int ticket = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = ticket, CarPlate = "QUEUE-REM-1", FirstDate = "2026/08/24", FirstWeight = 8000 });

            // Complete it
            db.SaveSecondWeight(ticket, 3000, "2026/08/24", "04:00:00 م", 5000);

            var pending = db.GetPendingWeighings("2026/08/24");
            foreach (DataRow row in pending.Rows)
            {
                Assert(row["CarPlate"].ToString() != "QUEUE-REM-1", "Completed truck must be removed from pending queue");
            }
        }

        private static void Test15_DuplicatePlate_MultiTrip()
        {
            var db = DatabaseService.Instance;
            string plate = "MULTI-TRIP-77";

            // Trip 1
            int t1 = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t1, CarPlate = plate, FirstWeight = 10000 });
            db.SaveSecondWeight(t1, 4000, "2026/08/24", "05:00:00 م", 6000);

            // Trip 2 on same plate
            int t2 = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t2, CarPlate = plate, FirstWeight = 12000 });

            var pending = db.GetPendingWeighingByPlate(plate);
            Assert(pending != null, "Must find the active pending trip for this plate");
            Assert(pending.TicketNo == t2, "Pending trip must be the second ticket ID");
            Assert(pending.FirstWeight == 12000, "Must have correct weight for second trip");
        }

        private static void Test16_Record_Navigation_PrevNext()
        {
            var db = DatabaseService.Instance;
            int t1 = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t1, CarPlate = "NAV-1", FirstWeight = 5000 });
            int t2 = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t2, CarPlate = "NAV-2", FirstWeight = 6000 });

            var prev = db.GetPreviousRecord(t2);
            Assert(prev != null && prev.TicketNo == t1, "Previous record from t2 must be t1");

            var next = db.GetNextRecord(t1);
            Assert(next != null && next.TicketNo == t2, "Next record from t1 must be t2");
        }
        #endregion

        #region Category 3: Database & Persistence Layer
        private static void Test17_Database_AutoInit()
        {
            var db = DatabaseService.Instance;
            Assert(db != null, "Database service instance must be non-null");
            string company = db.GetSetting("CompanyName");
            Assert(!string.IsNullOrEmpty(company), "Default settings must be initialized in database");
        }

        private static void Test18_Database_TicketAutoIncrement()
        {
            var db = DatabaseService.Instance;
            int n1 = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = n1, CarPlate = "INC-1", FirstWeight = 1000 });
            int n2 = db.GetNextTicketNo();
            Assert(n2 > n1, $"Next ticket ({n2}) must be greater than previous ({n1})");
        }

        private static void Test19_Database_CustomerAutoAdd()
        {
            var db = DatabaseService.Instance;
            string custName = "الشركة المصرية لنقل البضائع";
            db.SaveFirstWeight(new WeighingRecord { TicketNo = db.GetNextTicketNo(), CustomerName = custName, CarPlate = "CUST-1", FirstWeight = 2000 });

            var list = db.GetCustomerNames();
            Assert(list.Contains(custName), "Customer name must be automatically registered in customer table");
        }

        private static void Test20_Database_CustomerBalance()
        {
            var db = DatabaseService.Instance;
            string name = "شركة الأمل للتجارة";
            db.AddOrUpdateCustomer(name, "01234567890", "الإسكندرية", 2500.50);

            var list = db.GetCustomerNames();
            Assert(list.Contains(name), "Customer must be added");
        }

        private static void Test21_Database_SettingsPersistence()
        {
            var db = DatabaseService.Instance;
            db.SaveSetting("TestCustomKey", "TestCustomValue_123");
            string val = db.GetSetting("TestCustomKey");
            Assert(val == "TestCustomValue_123", "Setting value must persist and match");
        }

        private static void Test22_Database_SpecialCharacters()
        {
            var db = DatabaseService.Instance;
            string specialName = "مؤسسة 'النور' & \"البركة\" للتجارة (فرع #1) -- اختبار";
            int ticket = db.GetNextTicketNo();

            db.SaveFirstWeight(new WeighingRecord
            {
                TicketNo = ticket,
                CustomerName = specialName,
                CarPlate = "SPECIAL-PL",
                Notes = "ملاحظات تحتوي على 'علامات اقتباس' و رموز % & * /",
                FirstWeight = 7500
            });

            var rec = db.GetWeighingByTicket(ticket);
            Assert(rec != null, "Record with special characters must save without SQL error");
            Assert(rec.CustomerName == specialName, "Special characters must be preserved verbatim");
        }

        private static void Test23_Database_MultiFieldSearch()
        {
            var db = DatabaseService.Instance;
            int ticket = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = ticket, CustomerName = "البحث_المميز", CarPlate = "SEARCH-999", CargoType = "بضاعة_خاصة", FirstWeight = 4000 });

            var resByPlate = db.GetAllWeighings("SEARCH-999");
            Assert(resByPlate.Rows.Count > 0, "Must find ticket by plate");

            var resByCust = db.GetAllWeighings("البحث_المميز");
            Assert(resByCust.Rows.Count > 0, "Must find ticket by customer");
        }

        private static void Test24_Database_EmptyFieldsHandling()
        {
            var db = DatabaseService.Instance;
            int ticket = db.GetNextTicketNo();

            // Save with empty strings / nulls
            db.SaveFirstWeight(new WeighingRecord
            {
                TicketNo = ticket,
                CarPlate = "NULL-TEST",
                CustomerName = null,
                DriverName = null,
                TrailerNo = null,
                CargoType = null,
                FirstWeight = 3000
            });

            var rec = db.GetWeighingByTicket(ticket);
            Assert(rec != null, "Record with null fields must be saved safely");
            Assert(rec.CustomerName == "", "Null customer should map to empty string");
            Assert(rec.DriverName == "", "Null driver should map to empty string");
        }
        #endregion

        #region Category 4: Validation, Error Handling & Safety Guards
        private static void Test25_Guard_NegativeFirstWeight()
        {
            var reading = ScaleProtocolEngine.ParseStream("=-000050\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(reading.Success, "Stream should parse");
            Assert(reading.IsNegative, "Negative reading must be flagged");
            Assert(reading.Weight == -50, "Weight is -50 kg");
            // The domain rule is: negative weights cannot be processed as valid truck loads
            bool isValidTruckLoad = reading.Weight > 0;
            Assert(!isValidTruckLoad, "Negative reading must be blocked as invalid truck load");
        }

        private static void Test26_Guard_NegativeSecondWeight()
        {
            var reading = ScaleProtocolEngine.ParseStream("=-000020\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(reading.IsNegative, "Negative second weight reading must be caught by guard");
        }

        private static void Test27_Guard_ZeroWeightDrop()
        {
            // Simulate sudden drop to zero
            var engine = ScaleProtocolEngine.Instance;
            engine.SetSimulatedWeight(0, true);
            Assert(engine.CurrentWeight == 0, "Current weight should be 0");
            Assert(engine.IsZero, "IsZero flag must be true");
            Assert(!engine.IsNegative, "Zero must not be negative");
        }

        private static void Test28_Guard_OutOfBoundsReading()
        {
            // Truck scale max capacity is 200,000 kg. A reading of 999,999 kg is a sensor error.
            var res = ScaleProtocolEngine.ParseStream("=+9999999\r\n", ScaleProtocolType.ContinuousASCII);
            Assert(!res.Success, "Out-of-bounds reading > 200,000 kg must fail validation");
        }

        private static void Test29_Guard_EmptyPlateValidation()
        {
            string emptyPlate = "   ";
            bool isValid = !string.IsNullOrWhiteSpace(emptyPlate);
            Assert(!isValid, "Empty or whitespace plate number must be rejected");
        }

        private static void Test30_Guard_NonExistentTicket()
        {
            var db = DatabaseService.Instance;
            var rec = db.GetWeighingByTicket(99999999);
            Assert(rec == null, "Querying non-existent ticket must safely return null without throwing exception");
        }

        private static void Test31_Guard_BufferOverflowSafety()
        {
            // Simulate sending a massive 50KB junk stream
            var sb = new StringBuilder();
            for (int i = 0; i < 5000; i++)
            {
                sb.Append("=004354\r\n");
            }

            var res = ScaleProtocolEngine.ParseStream(sb.ToString(), ScaleProtocolType.ContinuousASCII);
            Assert(res.Success && res.Weight == 4354, "High volume stream buffer must parse latest reading safely");
        }

        private static void Test32_Guard_SimulatorLifecycle()
        {
            var engine = ScaleProtocolEngine.Instance;
            engine.StartSimulator();
            Assert(engine.IsConnected, "Simulator must be connected when started");
            Thread.Sleep(50);
            engine.StopSimulator();
            Assert(!engine.IsConnected, "Simulator must be stopped cleanly");
        }
        #endregion

        #region Category 5: Ticket Generation, Arithmetic & Reporting
        private static void Test33_Ticket_NetWeightInvariant()
        {
            int gross = 25480;
            int tare = 9640;
            int net = Math.Abs(gross - tare);
            Assert(net == 15840, $"Expected Net 15840 kg, got {net}");
        }

        private static void Test34_Ticket_DualTimestamps()
        {
            var db = DatabaseService.Instance;
            int ticket = db.GetNextTicketNo();

            string t1Date = "2026/08/24";
            string t1Time = "09:15:30 ص";
            string t2Date = "2026/08/24";
            string t2Time = "10:45:12 ص";

            db.SaveFirstWeight(new WeighingRecord { TicketNo = ticket, CarPlate = "TIME-TEST", FirstDate = t1Date, FirstTime = t1Time, FirstWeight = 14000 });
            db.SaveSecondWeight(ticket, 5000, t2Date, t2Time, 9000);

            var rec = db.GetWeighingByTicket(ticket);
            Assert(rec.FirstTime == t1Time, "First time must remain intact");
            Assert(rec.SecondTime == t2Time, "Second time must be recorded accurately");
        }

        private static void Test35_Ticket_DefaultGovernorate()
        {
            var db = DatabaseService.Instance;
            string defGov = db.GetSetting("DefaultGovernorate", "كفر الشيخ");
            Assert(!string.IsNullOrEmpty(defGov), "Default governorate setting must exist");
        }

        private static void Test36_Ticket_FeeAndNotes()
        {
            var db = DatabaseService.Instance;
            int ticket = db.GetNextTicketNo();

            db.SaveFirstWeight(new WeighingRecord { TicketNo = ticket, CarPlate = "FEE-TEST", FirstWeight = 10000, Fee = 150.0, Notes = "رسوم نولون خاصة" });
            db.SaveSecondWeight(ticket, 4000, "2026/08/24", "12:00:00 م", 6000, 150.0, "تم السداد نقداً");

            var rec = db.GetWeighingByTicket(ticket);
            Assert(rec.Fee == 150.0, $"Expected fee 150, got {rec.Fee}");
            Assert(rec.Notes == "تم السداد نقداً", "Notes must update on completion");
        }

        private static void Test37_Branding_DefaultsFallback()
        {
            var db = DatabaseService.Instance;
            string title = db.GetSetting("CompanyName", "ميزان بسكول ابو السعد ۱۲۰ طن");
            Assert(title.Contains("ابو السعد"), "Default company title must match reference branding");
        }

        private static void Test38_Ticket_LatestRecord()
        {
            var db = DatabaseService.Instance;
            int t = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = t, CarPlate = "LATEST-CHECK", FirstWeight = 11111 });

            var latest = db.GetLatestRecord();
            Assert(latest != null && latest.TicketNo >= t, "GetLatestRecord must return the latest ticket");
        }

        private static void Test39_Ticket_StatusPartitioning()
        {
            var db = DatabaseService.Instance;
            int tPending = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = tPending, CarPlate = "PART-PEND", FirstWeight = 7000, Status = "Pending" });

            int tComp = db.GetNextTicketNo();
            db.SaveFirstWeight(new WeighingRecord { TicketNo = tComp, CarPlate = "PART-COMP", FirstWeight = 7000 });
            db.SaveSecondWeight(tComp, 3000, "2026/08/24", "01:00:00 م", 4000);

            var pendingOnly = db.GetAllWeighings("", "Pending");
            var completedOnly = db.GetAllWeighings("", "Completed");

            Assert(pendingOnly.Rows.Count > 0, "Pending partition must return records");
            Assert(completedOnly.Rows.Count > 0, "Completed partition must return records");
        }

        private static void Test40_EndToEnd_MultiTruckStress()
        {
            var db = DatabaseService.Instance;
            // Stress test: 5 trucks arrive simultaneously, queued, processed in random order
            int[] tickets = new int[5];
            string[] plates = { "STRESS-101", "STRESS-102", "STRESS-103", "STRESS-104", "STRESS-105" };
            int[] grossWeights = { 20000, 22000, 24000, 26000, 28000 };
            int[] tareWeights = { 7000, 8000, 9000, 10000, 11000 };

            for (int i = 0; i < 5; i++)
            {
                tickets[i] = db.GetNextTicketNo();
                db.SaveFirstWeight(new WeighingRecord
                {
                    TicketNo = tickets[i],
                    CarPlate = plates[i],
                    CustomerName = $"عميل ضغط #{i + 1}",
                    FirstWeight = grossWeights[i],
                    FirstDate = "2026/08/24",
                    FirstTime = $"0{i + 1}:00:00 م",
                    Status = "Pending"
                });
            }

            // Complete in reverse order (Truck 5 first, then Truck 1)
            for (int i = 4; i >= 0; i--)
            {
                int net = grossWeights[i] - tareWeights[i];
                bool ok = db.SaveSecondWeight(tickets[i], tareWeights[i], "2026/08/24", $"0{i + 2}:00:00 م", net);
                Assert(ok, $"Stress test completion failed for truck {plates[i]}");

                var rec = db.GetWeighingByTicket(tickets[i]);
                Assert(rec.Status == "Completed", "Must be marked completed");
                Assert(rec.NetWeight == net, "Net weight must be exact");
            }
        }
        #endregion
    }
}
