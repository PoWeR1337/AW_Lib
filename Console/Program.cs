using Spectre.Console;
using AW_Lib;
class Program
{
    static void Main(string[] args)
    {
        // Kontruktor

        MainMenuKonstruktor();
        
    }

    
    /// <summary>
    /// Title und Version
    /// </summary>
    /// 

    // Start Konstruktor
    static void MainMenuKonstruktor()
    {
        Titel();
        H_Menu();
    }

    static void MenuKonstruktor()
    {

    }

   public static void Titel()
    {
        // IP Adresse
      string IP =  A_IP.GetPublicIpAddress();
        // Erstelle eine Instanz der Klasse, die das Interface implementiert
        IAppInfo appInfo = new AppInfo();

        string Titel =  appInfo.Title;
        appInfo.Title = "AWET";
        appInfo.Version = "0.1";


        // Header with AWET
        var header = new FigletText(appInfo.Title)
            .Centered()
            .Color(Color.Blue);

        // APP Version separator
        var versionSeparator = new Rule($"[red]{appInfo.Version}[/]")
            .Centered();

        // IP 

        appInfo.currentDate = DateTime.Now;
        // APP Version separator
        var Separator = new Rule($"[red]{appInfo.currentDate}[/]")
            .Centered();

        // Render the interface
        AnsiConsole.Write(header);
        AnsiConsole.Write(versionSeparator);
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
            .AddChoices(new[] { "Telegram Bot", "Downloader", "Option 3" });


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

                break;
            default:
                MainMenuKonstruktor();
                Console.Clear();
                break;
        }
    }
}
