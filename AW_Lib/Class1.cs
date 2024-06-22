using AW_Lib;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
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
               /// <summary>
               /// TOdo -> Initialisieren von :
               /// 
               /// </summary>
               /// <param name="args"></param>

        public static void Main(string[] args)
        {
           
            // Database Connect
           
            // Version
            IAppInfo appInfo = new AppInfo();
            appInfo.Version = 0123;
            

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
    string Ping { get; set; }
}

    public class AppInfo : IAppInfo
    {
        public string Title { get; set; } = "AW-E";
        public double Version { get; set; } = 0.1;
        public DateTime currentDate { get; set; } = DateTime.Now;
        public string Author { get; set; } = "AW";
        public string Updated { get; set; } = "21.06.2024";
    public bool DBConnect { get; set; } = true;
        public string dberror { get; set; } = "0";
    public string Ping { get; set; } = "0";

   

    }
/// <summary>
/// Datenbank
/// |MongoDB|
/// </summary>  
/// 
public class Datenbank
{

  public string rs { get; set; } = "";



    private readonly MongoClient _client;

    public Datenbank()
    {
        ConnectToMongoDB(Get_client());
    }

    private MongoClient Get_client()
    {
        return _client;
    }

    private void ConnectToMongoDB(MongoClient _client)
    {
        // Verbindung check
#pragma warning disable CS0219 // Variable ist zugewiesen, der Wert wird jedoch niemals verwendet
        bool DB = false;
        // #pragma warning restore CS0219 // Variable ist zugewiesen, der Wert wird jedoch niemals verwendet
        var error = "";

            const string connectionUri = "mongodb+srv://power:<password>@aw.71zfrso.mongodb.net/?retryWrites=true&w=majority&appName=aw";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        _client = new MongoClient(settings);

            try
            {
                var result = _client.GetDatabase("aw").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                rs = (string)result;
                AppInfo info = new AppInfo();
            info.Ping = rs;
                DB = true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            

        }

    }
    
       
       
           public static async Task<List<string>> ListDatabase()
            {
            
                var client = new MongoClient("mongodb+srv://power:<password>@aw.71zfrso.mongodb.net/?retryWrites=true&w=majority&appName=aw");

                var databases = await client.ListDatabasesAsync();

                List<string> databaseList = new List<string>();

                foreach (var database in databases.ToBson())
                {
                    databaseList.Add(database.ToString());
                }

                return databaseList;
            }
 
         }
         
    

        

    


    // Get set Methode für Error und Verbindung

    public class DBInfo
    {
        public string Fehler { get; set; } = "";
        public bool IsActive { get; set; } = false;

        // Constructor to initialize the properties
        public DBInfo(string error, bool DB)
        {
            error = Fehler;
            DB = IsActive;
            IAppInfo a = new AppInfo();
            a.DBConnect = IsActive;
            a.dberror = Fehler;

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
#pragma warning restore IDE0063 // Einfache using-Anweisung verwenden
        }
    }


   

    // ENDE INFO



    


