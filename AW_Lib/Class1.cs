using AW_Lib;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using static Datenbank;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AW_Lib

/// AW Libary 
///Update : 7.4.24
///ToDo :
///1 APP Info -> Logging, Version
///2.Telegram bot implentierung
///3.  

{
    public class Konstruktor
    {
     

        public static void Main(string[] args)
        {
           
            // Database Connect
            Datenbank.MongoDB();
            // Version
            IAppInfo appInfo = new AppInfo();
            appInfo.Version = 0.12;
         
        }        
    } 
    }
    /// <summary>
    /// APP Info
    /// </summary>
    public interface IAppInfo
    {
        string Title { get; set; }
        double Version { get; set; }
        string Author { get; set; }
        string Updated { get; set; }
        DateTime currentDate { get; set; }
        bool DBConnect { get; set; }
        string dberror { get; set; }

    }

    public class AppInfo : IAppInfo
    {
        public string Title { get; set; } = "AW-E";
        public double Version { get; set; } = 0.1;
        public DateTime currentDate { get; set; } = DateTime.Now;
        public string Author { get; set; } = "AW";
        public string Updated { get; set; } = "21.06.2024";
        public bool DBConnect { get; set; }
        public string dberror { get; set; } = "1";

    }
    /// <summary>
    /// Datenbank
    /// |MongoDB|
    /// </summary>  
    /// 
    public class Datenbank
    {
             
        // Datenbank Connect 
       public static void MongoDB()
        {
            
            // Verbindung check
            #pragma warning disable CS0219 // Variable ist zugewiesen, der Wert wird jedoch niemals verwendet
            bool DB = false;
            #pragma warning restore CS0219 // Variable ist zugewiesen, der Wert wird jedoch niemals verwendet
             var error = "";

            const string connectionUri = "mongodb+srv://admin:<password>@awdata.yt88x.mongodb.net/?retryWrites=true&w=majority&appName=awdata";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                DB = true;
            DBInfo db = new DBInfo(error, DB);
            db.IsActive = true;
        }
            catch (Exception ex)
            {   
                DBInfo db = new DBInfo(error,DB); 
                db.Fehler = ex.Message;
                DB = false;           
            db.IsActive = false;
            }
              
        }
         public void DBTest()
    {

    }

            // Get set Methode für Error und Verbindung

        public class DBInfo
        {
            public string Fehler { get; set; }
            public bool IsActive { get; set; }

            // Constructor to initialize the properties
            public DBInfo(string error, bool DB)
            {
                Fehler = error;
                IsActive = DB;
                IAppInfo a = new AppInfo();
                a.DBConnect = DB;
                a.dberror = Fehler;

            }

        }

    }


    

    /// <summary>
    /// IP
    /// </summary>
    public class A_IP
    {
    public static string GetPublicIpAddress()
    {
        using (var client = new WebClient())
        {
            try
            {
                // Die externe IP-Adresse von ipinfo.io abrufen
                string response = client.DownloadString("https://ipinfo.io/ip");
                // Die Antwort (die IP-Adresse) parsen
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
   

    // ENDE INFO



    


