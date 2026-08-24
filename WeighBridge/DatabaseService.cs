using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

namespace Weighbridge
{
    public class WeighingRecord
    {
        public int Id { get; set; }
        public int TicketNo { get; set; }
        public string CustomerName { get; set; }
        public string CarPlate { get; set; }
        public string TrailerNo { get; set; }
        public string DriverName { get; set; }
        public string CargoType { get; set; }
        public string Governorate { get; set; }
        public int FirstWeight { get; set; }
        public string FirstDate { get; set; }
        public string FirstTime { get; set; }
        public int SecondWeight { get; set; }
        public string SecondDate { get; set; }
        public string SecondTime { get; set; }
        public int NetWeight { get; set; }
        public string Status { get; set; } // "Pending", "Completed"
        public double Fee { get; set; }
        public string Notes { get; set; }
        public string CreatedAt { get; set; }
    }

    public class DatabaseService
    {
        private static DatabaseService _instance;
        public static DatabaseService Instance => _instance ?? (_instance = new DatabaseService());

        private readonly string _dbPath;
        private readonly string _connectionString;

        public DatabaseService()
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            _dbPath = Path.Combine(appDir, "weighbridge.db");
            _connectionString = $"Data Source={_dbPath};Version=3;";
            InitializeDatabase();
        }

        private SqliteConnection GetConnection()
        {
            var conn = new SqliteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public void InitializeDatabase()
        {
            using (var conn = GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Weighings (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            TicketNo INTEGER NOT NULL,
                            CustomerName TEXT,
                            CarPlate TEXT NOT NULL,
                            TrailerNo TEXT,
                            DriverName TEXT,
                            CargoType TEXT,
                            Governorate TEXT,
                            FirstWeight INTEGER DEFAULT 0,
                            FirstDate TEXT,
                            FirstTime TEXT,
                            SecondWeight INTEGER DEFAULT 0,
                            SecondDate TEXT,
                            SecondTime TEXT,
                            NetWeight INTEGER DEFAULT 0,
                            Status TEXT NOT NULL DEFAULT 'Pending',
                            Fee REAL DEFAULT 0,
                            Notes TEXT,
                            CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                        );

                        CREATE TABLE IF NOT EXISTS Customers (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT UNIQUE NOT NULL,
                            Phone TEXT,
                            Address TEXT,
                            Balance REAL DEFAULT 0,
                            Notes TEXT
                        );

                        CREATE TABLE IF NOT EXISTS Settings (
                            Key TEXT PRIMARY KEY,
                            Value TEXT
                        );

                        CREATE TABLE IF NOT EXISTS Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT UNIQUE NOT NULL,
                            Password TEXT NOT NULL,
                            Role TEXT DEFAULT 'Operator',
                            FullName TEXT
                        );
                    ";
                    cmd.ExecuteNonQuery();
                }

                SeedDefaultSettings(conn);
            }
        }

        private void SeedDefaultSettings(SqliteConnection conn)
        {
            var defaults = new Dictionary<string, string>
            {
                { "CompanyName", "ميزان بسكول ابو السعد ۱۲۰ طن" },
                { "CompanySubtitle", "العنوان فوه كفر الشيخ ٠١٠٩٢١٨٠١٤٦ ايمن / ٠١٠٦٢٥٥١١٨٩ محمد" },
                { "FooterText", "هذا البرنامج صنع خصيصا لشركة اولاد الغلباني الحديثة بدمنهور" },
                { "DefaultGovernorate", "كفر الشيخ" },
                { "PortName", "COM1" },
                { "BaudRate", "9600" },
                { "DataBits", "8" },
                { "Parity", "None" },
                { "StopBits", "One" },
                { "Handshake", "None" },
                { "ScaleProtocol", "ContinuousASCII" },
                { "SimulatorEnabled", "False" },
                { "RxEventInterval", "12" },
                { "InputLen", "0" }
            };

            foreach (var kv in defaults)
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT OR IGNORE INTO Settings (Key, Value) VALUES (@Key, @Value);";
                    cmd.Parameters.AddWithValue("@Key", kv.Key);
                    cmd.Parameters.AddWithValue("@Value", kv.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string GetSetting(string key, string defaultValue = "")
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Value FROM Settings WHERE Key = @Key LIMIT 1;";
                cmd.Parameters.AddWithValue("@Key", key);
                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : defaultValue;
            }
        }

        public void SaveSetting(string key, string value)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT OR REPLACE INTO Settings (Key, Value) VALUES (@Key, @Value);";
                cmd.Parameters.AddWithValue("@Key", key);
                cmd.Parameters.AddWithValue("@Value", value ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        public int GetNextTicketNo()
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT IFNULL(MAX(TicketNo), 0) + 1 FROM Weighings;";
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public int SaveFirstWeight(WeighingRecord record)
        {
            if (record.TicketNo <= 0)
            {
                record.TicketNo = GetNextTicketNo();
            }

            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO Weighings (
                        TicketNo, CustomerName, CarPlate, TrailerNo, DriverName,
                        CargoType, Governorate, FirstWeight, FirstDate, FirstTime,
                        SecondWeight, SecondDate, SecondTime, NetWeight, Status, Fee, Notes
                    ) VALUES (
                        @TicketNo, @CustomerName, @CarPlate, @TrailerNo, @DriverName,
                        @CargoType, @Governorate, @FirstWeight, @FirstDate, @FirstTime,
                        0, '', '', 0, 'Pending', @Fee, @Notes
                    );
                    SELECT last_insert_rowid();
                ";

                cmd.Parameters.AddWithValue("@TicketNo", record.TicketNo);
                cmd.Parameters.AddWithValue("@CustomerName", record.CustomerName ?? "");
                cmd.Parameters.AddWithValue("@CarPlate", record.CarPlate ?? "");
                cmd.Parameters.AddWithValue("@TrailerNo", record.TrailerNo ?? "");
                cmd.Parameters.AddWithValue("@DriverName", record.DriverName ?? "");
                cmd.Parameters.AddWithValue("@CargoType", record.CargoType ?? "");
                cmd.Parameters.AddWithValue("@Governorate", record.Governorate ?? "");
                cmd.Parameters.AddWithValue("@FirstWeight", record.FirstWeight);
                cmd.Parameters.AddWithValue("@FirstDate", record.FirstDate ?? "");
                cmd.Parameters.AddWithValue("@FirstTime", record.FirstTime ?? "");
                cmd.Parameters.AddWithValue("@Fee", record.Fee);
                cmd.Parameters.AddWithValue("@Notes", record.Notes ?? "");

                cmd.ExecuteNonQuery();

                if (!string.IsNullOrEmpty(record.CustomerName))
                {
                    AddOrUpdateCustomer(record.CustomerName);
                }

                return record.TicketNo;
            }
        }

        public bool SaveSecondWeight(int ticketNo, int secondWeight, string secondDate, string secondTime, int netWeight, double fee = 0, string notes = "")
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    UPDATE Weighings SET
                        SecondWeight = @SecondWeight,
                        SecondDate = @SecondDate,
                        SecondTime = @SecondTime,
                        NetWeight = @NetWeight,
                        Status = 'Completed',
                        Fee = @Fee,
                        Notes = @Notes
                    WHERE TicketNo = @TicketNo;
                ";

                cmd.Parameters.AddWithValue("@SecondWeight", secondWeight);
                cmd.Parameters.AddWithValue("@SecondDate", secondDate ?? "");
                cmd.Parameters.AddWithValue("@SecondTime", secondTime ?? "");
                cmd.Parameters.AddWithValue("@NetWeight", netWeight);
                cmd.Parameters.AddWithValue("@Fee", fee);
                cmd.Parameters.AddWithValue("@Notes", notes ?? "");
                cmd.Parameters.AddWithValue("@TicketNo", ticketNo);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public DataTable GetPendingWeighings(string dateFilter = null)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                if (string.IsNullOrEmpty(dateFilter))
                {
                    cmd.CommandText = "SELECT TicketNo, CarPlate, FirstWeight, FirstTime, FirstDate, CustomerName, CargoType FROM Weighings WHERE Status = 'Pending' ORDER BY Id DESC;";
                }
                else
                {
                    cmd.CommandText = "SELECT TicketNo, CarPlate, FirstWeight, FirstTime, FirstDate, CustomerName, CargoType FROM Weighings WHERE Status = 'Pending' AND FirstDate LIKE @DateFilter ORDER BY Id DESC;";
                    cmd.Parameters.AddWithValue("@DateFilter", "%" + dateFilter + "%");
                }

                var dt = new DataTable();
                using (var adapter = new SqliteDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
        }

        public WeighingRecord GetWeighingByTicket(int ticketNo)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Weighings WHERE TicketNo = @TicketNo LIMIT 1;";
                cmd.Parameters.AddWithValue("@TicketNo", ticketNo);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapRecord(reader);
                    }
                }
            }
            return null;
        }

        public WeighingRecord GetPendingWeighingByPlate(string plate)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Weighings WHERE CarPlate = @CarPlate AND Status = 'Pending' ORDER BY Id DESC LIMIT 1;";
                cmd.Parameters.AddWithValue("@CarPlate", plate);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapRecord(reader);
                    }
                }
            }
            return null;
        }

        public WeighingRecord GetPreviousRecord(int currentTicketNo)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Weighings WHERE TicketNo < @TicketNo ORDER BY TicketNo DESC LIMIT 1;";
                cmd.Parameters.AddWithValue("@TicketNo", currentTicketNo);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return MapRecord(reader);
                }
            }
            return null;
        }

        public WeighingRecord GetNextRecord(int currentTicketNo)
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Weighings WHERE TicketNo > @TicketNo ORDER BY TicketNo ASC LIMIT 1;";
                cmd.Parameters.AddWithValue("@TicketNo", currentTicketNo);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return MapRecord(reader);
                }
            }
            return null;
        }

        public WeighingRecord GetLatestRecord()
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Weighings ORDER BY TicketNo DESC LIMIT 1;";
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return MapRecord(reader);
                }
            }
            return null;
        }

        public DataTable GetAllWeighings(string searchText = "", string statusFilter = "All")
        {
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                string sql = "SELECT TicketNo AS 'رقم التذكرة', CarPlate AS 'رقم اللوحة', CustomerName AS 'العميل', CargoType AS 'نوع الشحنة', FirstWeight AS 'الوزن الأول', SecondWeight AS 'الوزن الثاني', NetWeight AS 'الصافي', FirstDate AS 'التاريخ', Status AS 'الحالة' FROM Weighings WHERE 1=1 ";

                if (!string.IsNullOrEmpty(searchText))
                {
                    sql += " AND (CarPlate LIKE @Q OR CustomerName LIKE @Q OR TicketNo LIKE @Q OR CargoType LIKE @Q) ";
                    cmd.Parameters.AddWithValue("@Q", "%" + searchText + "%");
                }

                if (statusFilter == "Pending")
                {
                    sql += " AND Status = 'Pending' ";
                }
                else if (statusFilter == "Completed")
                {
                    sql += " AND Status = 'Completed' ";
                }

                sql += " ORDER BY TicketNo DESC;";
                cmd.CommandText = sql;

                var dt = new DataTable();
                using (var adapter = new SqliteDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
        }

        public List<string> GetCustomerNames()
        {
            var list = new List<string>();
            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Name FROM Customers ORDER BY Name ASC;";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["Name"].ToString());
                    }
                }
            }
            return list;
        }

        public void AddOrUpdateCustomer(string name, string phone = "", string address = "", double balance = 0)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            using (var conn = GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO Customers (Name, Phone, Address, Balance)
                    VALUES (@Name, @Phone, @Address, @Balance)
                    ON CONFLICT(Name) DO UPDATE SET
                        Phone = CASE WHEN @Phone != '' THEN @Phone ELSE Customers.Phone END,
                        Address = CASE WHEN @Address != '' THEN @Address ELSE Customers.Address END;
                ";
                cmd.Parameters.AddWithValue("@Name", name.Trim());
                cmd.Parameters.AddWithValue("@Phone", phone ?? "");
                cmd.Parameters.AddWithValue("@Address", address ?? "");
                cmd.Parameters.AddWithValue("@Balance", balance);
                cmd.ExecuteNonQuery();
            }
        }

        private WeighingRecord MapRecord(SqliteDataReader reader)
        {
            return new WeighingRecord
            {
                Id = Convert.ToInt32(reader["Id"]),
                TicketNo = Convert.ToInt32(reader["TicketNo"]),
                CustomerName = reader["CustomerName"]?.ToString() ?? "",
                CarPlate = reader["CarPlate"]?.ToString() ?? "",
                TrailerNo = reader["TrailerNo"]?.ToString() ?? "",
                DriverName = reader["DriverName"]?.ToString() ?? "",
                CargoType = reader["CargoType"]?.ToString() ?? "",
                Governorate = reader["Governorate"]?.ToString() ?? "",
                FirstWeight = Convert.ToInt32(reader["FirstWeight"]),
                FirstDate = reader["FirstDate"]?.ToString() ?? "",
                FirstTime = reader["FirstTime"]?.ToString() ?? "",
                SecondWeight = Convert.ToInt32(reader["SecondWeight"]),
                SecondDate = reader["SecondDate"]?.ToString() ?? "",
                SecondTime = reader["SecondTime"]?.ToString() ?? "",
                NetWeight = Convert.ToInt32(reader["NetWeight"]),
                Status = reader["Status"]?.ToString() ?? "Pending",
                Fee = reader["Fee"] != DBNull.Value ? Convert.ToDouble(reader["Fee"]) : 0,
                Notes = reader["Notes"]?.ToString() ?? "",
                CreatedAt = reader["CreatedAt"]?.ToString() ?? ""
            };
        }
    }
}
