using AW_Lib;
using System;
using System.Net;

namespace AW_Lib

    /// AW Libary 
   ///Update : 7.4.24
   ///ToDo :
   ///1 APP Info -> Logging, Version
   ///2.Telegram bot implentierung
   ///3.  
    
{
   
    /// <summary>
    /// APP Info
    /// </summary>
    public interface IAppInfo
        {
            string Title { get; set; }
            string Version { get; set; }
            DateTime currentDate { get; set; }

        }

        public class AppInfo : IAppInfo
        {
        public string Title { get; set; } = "AW-E";
        public string Version { get; set; } = "0.0";
        public DateTime currentDate { get; set; } = DateTime.Now;

    }
        /// <summary>
        /// Client Infornmations
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

}

    


