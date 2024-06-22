using Spectre.Console;
using AW_Lib;
using Musik;
using MongoDB.Driver;
using MongoDB.Bson;
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
        var IPs = new Rule($"[red]{IP}[/]")
            .Centered();

        appInfo.currentDate = DateTime.Now;
        // APP Version separator
        var Separator = new Rule($"[red]{appInfo.currentDate}[/]")
            .Centered();

        // Render the interface
        AnsiConsole.Write(header);
        AnsiConsole.Write(versionSeparator);
        AnsiConsole.Write(IPs);
        AnsiConsole.Write(Separator);
    }

    List<string> tools = new List<string> { "Musik", "Verschlüsselung", "Passwort", "Telegram" };

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
            .AddChoices(new[] { "Musik", "Verschlüsselung", "Telegram" });


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
