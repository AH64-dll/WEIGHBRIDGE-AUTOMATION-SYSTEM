using System;
using System.Data;
using System.IO;

namespace Weighbridge.Tests
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("=== Starting Weighbridge Real-World Scenario Tests ===\n");
            int passed = 0;
            int failed = 0;

            // 1. Test Database Initialization
            try
            {
                var db = DatabaseService.Instance;
                Console.WriteLine("[TEST 1] Database Initialization: PASSED");
                passed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST 1] Database Initialization: FAILED ({ex.Message})");
                failed++;
            }

            // 2. Test First Weight Scenario (Truck arrives with full cargo)
            int ticketNo = 0;
            try
            {
                var db = DatabaseService.Instance;
                var record = new WeighingRecord
                {
                    TicketNo = db.GetNextTicketNo(),
                    CustomerName = "محمد الشبيبة",
                    CarPlate = "7968",
                    TrailerNo = "1234",
                    DriverName = "علي محمود",
                    CargoType = "حديد وخردة",
                    Governorate = "كفر الشيخ",
                    FirstWeight = 8778,
                    FirstDate = "2026/08/24",
                    FirstTime = "07:13:42 م",
                    Status = "Pending"
                };

                ticketNo = db.SaveFirstWeight(record);
                if (ticketNo > 0)
                {
                    Console.WriteLine($"[TEST 2] First Weight Recording (Ticket #{ticketNo}, Plate: 7968, Gross: 8778 kg): PASSED");
                    passed++;
                }
                else
                {
                    throw new Exception("TicketNo returned was <= 0");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST 2] First Weight Recording: FAILED ({ex.Message})");
                failed++;
            }

            // 3. Test Pending Queue Lookup
            try
            {
                var db = DatabaseService.Instance;
                var pending = db.GetPendingWeighings("2026/08/24");
                bool found = false;
                foreach (DataRow row in pending.Rows)
                {
                    if (row["CarPlate"].ToString() == "7968" && Convert.ToInt32(row["FirstWeight"]) == 8778)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    Console.WriteLine("[TEST 3] Pending Queue Verification: PASSED");
                    passed++;
                }
                else
                {
                    throw new Exception("Truck 7968 was not found in pending queue");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST 3] Pending Queue Verification: FAILED ({ex.Message})");
                failed++;
            }

            // 4. Test Second Weight & Net Weight Calculation Scenario (Empty truck departs)
            try
            {
                var db = DatabaseService.Instance;
                int secondWeight = 4354;
                int expectedNet = 8778 - 4354; // 4424 kg

                bool ok = db.SaveSecondWeight(ticketNo, secondWeight, "2026/08/24", "08:54:19 م", expectedNet, 50.0, "تمت التصفية بنجاح");
                var updated = db.GetWeighingByTicket(ticketNo);

                if (ok && updated != null && updated.Status == "Completed" && updated.NetWeight == 4424)
                {
                    Console.WriteLine($"[TEST 4] Second Weight & Net Calculation (Gross: {updated.FirstWeight}, Tare: {updated.SecondWeight}, Net: {updated.NetWeight} kg): PASSED");
                    passed++;
                }
                else
                {
                    throw new Exception($"Net weight was {updated?.NetWeight}, expected 4424");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST 4] Second Weight & Net Calculation: FAILED ({ex.Message})");
                failed++;
            }

            // 5. Test Scale Protocol Parsers
            try
            {
                var engine = ScaleProtocolEngine.Instance;
                int receivedWeight = 0;
                engine.OnWeightChanged += (w, s) => { receivedWeight = w; };

                // Test Continuous ASCII
                engine.Protocol = ScaleProtocolType.ContinuousASCII;
                // Simulate receiving bytes
                engine.SetSimulatedWeight(4354);
                if (engine.CurrentWeight == 4354)
                {
                    Console.WriteLine("[TEST 5] Scale Engine Protocol & Simulation: PASSED");
                    passed++;
                }
                else
                {
                    throw new Exception("Simulated weight mismatch");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST 5] Scale Engine Protocol: FAILED ({ex.Message})");
                failed++;
            }

            // 6. Test Customer Balance & Ledger
            try
            {
                var db = DatabaseService.Instance;
                db.AddOrUpdateCustomer("شركة النيل للمقاولات", "01001234567", "طنطا", 1500.0);
                var customers = db.GetCustomerNames();
                if (customers.Contains("شركة النيل للمقاولات") && customers.Contains("محمد الشبيبة"))
                {
                    Console.WriteLine("[TEST 6] Customer Management & Auto-sync: PASSED");
                    passed++;
                }
                else
                {
                    throw new Exception("Customer names missing in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST 6] Customer Management: FAILED ({ex.Message})");
                failed++;
            }

            // 7. Test Search & History Filter
            try
            {
                var db = DatabaseService.Instance;
                var results = db.GetAllWeighings("7968");
                if (results.Rows.Count > 0 && results.Rows[0]["رقم اللوحة"].ToString() == "7968")
                {
                    Console.WriteLine("[TEST 7] Ticket Search and History Filter: PASSED");
                    passed++;
                }
                else
                {
                    throw new Exception("Search did not return plate 7968");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEST 7] Ticket Search: FAILED ({ex.Message})");
                failed++;
            }

            Console.WriteLine($"\n==========================================");
            Console.WriteLine($"TOTAL TESTS: {passed + failed} | PASSED: {passed} | FAILED: {failed}");
            Console.WriteLine($"==========================================");

            return failed == 0 ? 0 : 1;
        }
    }
}
