using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AW_Lib
{
    public class LibraryInitializer
    {
        // Hier können Initialisierungsaufgaben hinzugefügt werden, falls benötigt
    }

    public interface IAppInfo
    {
        string Title { get; set; }
        double Version { get; set; }
        string Author { get; set; }
        string Updated { get; set; }
        DateTime CurrentDate { get; set; }
        bool DBConnect { get; set; }
        string DbError { get; set; }
        double Ping { get; set; }

        public string On { get; set; }
    }

    public class AppInfo : IAppInfo
    {
        public string Title { get; set; } = "AW-E";
        public double Version { get; set; } = 0.1;
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public string Author { get; set; } = "AW";
        public string Updated { get; set; } = "22.06.2024";
        public bool DBConnect { get; set; } = false;
        public string DbError { get; set; } = "0";
        public double Ping { get; set; } = 0;
        public string On { get; set; } = "";
    }

    // MongoDB Model
      
    public class Tool
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ToolId { get; set; } = "1337";

        public ObjectId Id { get; set; }
        public string Name { get; set; } = "AWET";
        public List<Subtool> Subtools { get; set; }
    }
    public class Subtool
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "1337";

        public string Name { get; set; } = "AW";
        public string Update { get; set; } = "";
        public string Author { get; set; } = "AW";
        public string GitHub { get; set; } = "";
        public string ExecutablePath { get; set; } = "C:\\Users\\aw\\Source\\Repos\\AW_Lib\\Console\\Program.cs";
        public string Arguments { get; set; } = "";
        public string WorkingDirectory { get; set; } = "";
    }
    public class SubtoolExecutor
    {
        private static void ExecuteSubtool(Subtool subtool)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = subtool.ExecutablePath,
                    Arguments = subtool.Arguments,
                    WorkingDirectory = subtool.WorkingDirectory,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
                    process.ErrorDataReceived += (sender, data) => Console.WriteLine($"Error: {data.Data}");

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing subtool: {ex.Message}");
            }
        }
    }

    // MongoDB Service

    public class Database
    {
        private readonly IMongoDatabase _database;

        public class DatabaseService
        {
            private readonly IMongoDatabase _database;

            public DatabaseService(string connectionString, string databaseName)
            {
                var client = new MongoClient(connectionString);
                _database = client.GetDatabase(databaseName);
            }

            public bool Ping()
            {    
                AppInfo Info = new AppInfo();

                try
                {
                    var pingCommand = new BsonDocument { { "ping", 1 } };
                    var pingResult = _database.RunCommand<BsonDocument>(pingCommand);
                    var okValue = pingResult["ok"];
                    if (okValue is BsonInt32 okInt)
                    {
                       
                        Info.DBConnect = true;
                        return okInt.Value == 1;
                    }
                    else if (okValue is BsonDouble okDouble)
                    {
                      
                        Info.DBConnect = true;
                        return okDouble.Value == 1.0;
                    }
                    else
                    {
                        throw new InvalidOperationException("Unexpected type for 'ok' value");
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"MongoDB Ping Error: {ex.Message}");
                    return false;
                }
            }

            public IMongoCollection<T> GetCollection<T>(string name) where T : class
            {
                try
                {
                    return _database.GetCollection<T>(name);
                }
                catch (MongoException ex)
                {
                    Console.WriteLine($"Error retrieving collection '{name}': {ex.Message}");
                    throw; // Optionally handle or log the exception further
                }
            }
        }

    }

    // IP Address Helper

    public class A_IP
    {
        public static string GetPublicIpAddress()
        {
            using (var client = new WebClient())
            {
                try
                {
                    string response = client.DownloadString("https://ipinfo.io/ip");
                    return response.Trim();
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"Error retrieving public IP address: {ex.Message}");
                    return "None";
                }
            }
        }
    }
}
