using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AW_Lib
{
    public class LibraryInitializer
    {
        // TOdo -> Initialisieren von :
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
        public double Ping { get; set; } = 00;
    }

    // MongoDB

   
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
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<Subtool> Subtools { get; set; }
    }
    
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
                _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
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