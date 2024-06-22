using Spectre.Console;
using AW_Lib;
using Musik;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
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
              MainMenuKonstruktor(); 
                DB.Connect();   
    }
    

    // Start Konstruktor
    static void MainMenuKonstruktor()
    {
        
        Titel();

       // H_Menu( );      
    }


   public static void Titel()
    {
        string IP;
        // IP Adresse
        try
        {
            IP =  A_IP.GetPublicIpAddress();
        } catch(Exception)
        {
             IP = "/";
        }
     
        // Erstelle eine Instanz der Klasse, die das Interface implementiert
        IAppInfo appInfo = new AppInfo();
        string Titel =  appInfo.Title;
        appInfo.Title = "AWET";
        appInfo.Version = 0.2;
        bool db = appInfo.DBConnect;
        string update = appInfo.Updated;
        string autor = appInfo.Author;
        double ping = appInfo.Ping;
        string On = "";
        //Bool true = Online
        if(db = true)
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
        var DBC = new Rule($"[Green]{On}[/]")
            .Centered();
        // Autor
        var AT = new Rule($"[red]{autor}[/]")
    .Centered();
        //Datum
        appInfo.CurrentDate = DateTime.Now;
        // APP Version separator
        var Separator = new Rule($"[red]{appInfo.Updated}[/]")
            .Centered();

        // Render the interface
        AnsiConsole.Write(header);
        AnsiConsole.Write(DBC);
        AnsiConsole.Write(IPs);
        AnsiConsole.Write(Separator);
    }
public class DB
{
    private static DatabaseService _dbService;

        public static void Connect()
        {
            AppInfo Info = new AppInfo();
            // Initialize database service
            _dbService = new DatabaseService("mongodb+srv://power:Joanna1337,,.@aw.71zfrso.mongodb.net/?retryWrites=true&w=majority&appName=aw", "aw");

            if (_dbService.Ping())
            {
                Info.DBConnect = true;
            }
            else
            {
                Info.DBConnect = false;
                Console.Clear();
                MainMenuKonstruktor();
                return;
            }

            // Offline Tools
            var Offtools = new List<Tool>
        {
            new Tool
            {
                Name = "Info",
                Subtools = new List<Subtool>
                {
                    new Subtool
                    {
                        Name = "System Info",
                        Update = "22.06.204",
                        Author = "AW",
                        GitHub = "/"
                    },
                    new Subtool
                    {
                        Name = "Netwerk",
                        Update = "22.06.2024",
                        Author = "AW",
                        GitHub = "/"
                    }
                }
            },
                new Tool
            {
                Name = "Script",
                Subtools = new List<Subtool>
                {
                    new Subtool
                    {
                        Name = "Hash Algorithmus",
                        Update = "/",
                        Author = "AW",
                        GitHub = "/"
                    },
                    new Subtool
                    {
                        Name = "Passwort",
                        Update = "2024-06-01",
                        Author = "Author6",
                        GitHub = "/"
                    }
                } },

            new Tool
            {
                Name = "Einstellungen",
                Subtools = new List<Subtool>
                {
                    new Subtool
                    {
                        Name = "Design",
                        Update = "/",
                        Author = "AW",
                        GitHub = "/"
                    },
                    new Subtool
                    {
                        Name = "Löschen",
                        Update = "/",
                        Author = "AW",
                        GitHub = "/"
                    },
                    new Subtool
                    {
                        Name = "Offline speichern",
                        Update = "/",
                        Author = "AW",
                        GitHub = "/"
                    }
                }
            } };
        


            var toolsCollection = _dbService.GetCollection<Tool>("Tool");
            var tools = toolsCollection.Find(tool => true).ToList();

            // Merge tools and Offtools, avoiding duplicates
            foreach (var offTool in Offtools)
            {
                if (!tools.Any(t => t.Name == offTool.Name))
                {
                    tools.Add(offTool);
                }
            }

            var toolMenu = new SelectionPrompt<string>()
                .Title("Select a [green]Tool[/] to see details:")
                .AddChoices(tools.Select(t => t.Name).ToArray());

            var selectedToolName = AnsiConsole.Prompt(toolMenu);
            var selectedTool = tools.First(t => t.Name == selectedToolName);

            var subtoolMenu = new SelectionPrompt<string>()
                .Title($"Select a [green]Subtool[/] of [blue]{selectedTool.Name}[/] to see details:")
                .AddChoices(selectedTool.Subtools.Select(st => st.Name).ToArray());

            var selectedSubtoolName = AnsiConsole.Prompt(subtoolMenu);
            var selectedSubtool = selectedTool.Subtools.First(st => st.Name == selectedSubtoolName);

            var table = new Table();
            table.AddColumn("Property");
            table.AddColumn("Value");

            table.AddRow("Name", selectedSubtool.Name);
            table.AddRow("Update", selectedSubtool.Update);
            table.AddRow("Author", selectedSubtool.Author);
            table.AddRow("GitHub", selectedSubtool.GitHub);

            AnsiConsole.Write(table);
        }
    }

    // Local Ip Adresse

    /// <summary>
    /// Menu
    /// </summary>
    /// 

    //list all Tools
    static void H_Menu()
    {
              
       // DisplayTools();

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
