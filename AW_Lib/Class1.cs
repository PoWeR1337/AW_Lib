using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
    }

    // MongoDB Model

    public class Subtool
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Update { get; set; }
        public string Author { get; set; }
        public string GitHub { get; set; }
    }

    public class Tool
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ToolId { get; set; }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<Subtool> Subtools { get; set; }
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
                try
                {
                    var pingCommand = new BsonDocument { { "ping", 1 } };
                    var pingResult = _database.RunCommand<BsonDocument>(pingCommand);
                    return pingResult["ok"].AsDouble == 1.0;
                }
                catch (MongoException ex)
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
