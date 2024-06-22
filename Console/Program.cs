using Spectre.Console;
using AW_Lib;
using Musik;
using MongoDB.Driver;
using MongoDB.Bson;
using static Datenbank;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Collections;
class Program
{    
    
    /// <summary>
    /// Main Konstruktor
    /// Menu Konstruktor
    /// Datenbank Connect
    /// Title und Version
    /// </summary>
    /// 


    static void Main(string[] args)
    {
        // Kontruktor
        // Synchronous
        AnsiConsole.Status()
            .Start("Thinking...", ctx =>
            {
 
                // Update the status and spinner
                ctx.Status("Laden...");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));

            });
        MainMenuKonstruktor();       
    }
          // MongoDB
          
    
    // Start Konstruktor
    static void MainMenuKonstruktor()
    {
       
        Titel();
       
        H_Menu();
       
    }



   public static void Titel()
    {
        // IP Adresse
      string IP =  A_IP.GetPublicIpAddress();
        // Erstelle eine Instanz der Klasse, die das Interface implementiert
        IAppInfo appInfo = new AppInfo();

        string Titel =  appInfo.Title;
        appInfo.Title = "AWET";
        appInfo.Version = 0.2;
        bool db = appInfo.DBConnect;
        string update = appInfo.Updated;
        string autor = appInfo.Author;
        string ping = appInfo.Ping;
        string On = "Offline ";
        //Bool true = Online
        if(db == true)
        {
           On = "Online ";
        } 
       
        // Header with AWET
        var header = new FigletText(appInfo.Title)
            .Centered()
            .Color(Color.Blue);

        // APP Version separator
        var versionSeparator = new Rule($"[red]{appInfo.Version}[/]")
            .Centered();

        // IP
        // 
        // APP Version separator
        //IP
        var IPs = new Rule($"[red]{IP}[/]")
            .Centered();
        //IP
        //Database string 
        var DBC = new Rule($"[red]{On + " | Ping : " + ping}[/]")
            .Centered();
        // Autor
        var AT = new Rule($"[red]{autor}[/]")
    .Centered();
        //Datum
        appInfo.currentDate = DateTime.Now;
        // APP Version separator
        var Separator = new Rule($"[red]{appInfo.Updated}[/]")
            .Centered();

        // Render the interface
        AnsiConsole.Write(header);
        AnsiConsole.Write(DBC);
        AnsiConsole.Write(IPs);
        AnsiConsole.Write(Separator);
    }

 

    
    // Local Ip Adresse

    /// <summary>
    /// Menu
    /// </summary>
    static void H_Menu()
    {
       
 

        // Create a menu
        var menu = new SelectionPrompt<string>()
            .Title("[blue]Menü:[/]")
            .PageSize(5)
            .AddChoices(new[] {"Musik","Verschlüsselung","Dowloader" });


        var selectedOption = AnsiConsole.Prompt(menu);

        // Output the selected option
        AnsiConsole.MarkupLine($"[yellow]Starte:[/] {selectedOption}");

        switch ( selectedOption )
        {
            case "Telegram Bot":
                Console.Clear();
                var header = new FigletText("TeleGram Bot")
            .Centered()
            .Color(Color.Blue);
                CTelegegram.TelegramConsole.Konstruktor();
                break;
            case "Downloader":
                Console.Clear();
                MainMenuKonstruktor();
                break;
            case "Musik":
                Console.Clear();
                Musik.MusikConsole.konstrukt();
                break;
            default:
                Console.Clear();
                MainMenuKonstruktor();
                break;
        }
    }
}
