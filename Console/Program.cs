using Spectre.Console;
using AW_Lib;
class Program
{
    static void Main(string[] args)
    {
        // Kontruktor

        Titel();
      //  H_Menu();
    }

    
    /// <summary>
    /// Title und Version
    /// </summary>
   public static void Titel()
    {
        // Erstelle eine Instanz der Klasse, die das Interface implementiert
        IAppInfo appInfo = new AppInfo();

        string Titel =  appInfo.Title;
        appInfo.Title = "AWET";
        appInfo.Version = "0.1";


        // Header with AWET
        var header = new FigletText(appInfo.Title)
            .Centered()
            .Color(Color.Blue);

        // Version separator
        var versionSeparator = new Rule($"[red]{appInfo.Version}[/]")
            .Centered();


        // Render the interface
        AnsiConsole.Write(header);
        AnsiConsole.Write(versionSeparator);


    }

    /// <summary>
    /// Menu
    /// </summary>
    static void H_Menu()
    {
        
        // Create a menu
        var menu = new SelectionPrompt<string>()
            .Title("[blue]Menü:[/]")
            .PageSize(5)
            .AddChoices(new[] { "Option 1", "Option 2", "Option 3" });


        var selectedOption = AnsiConsole.Prompt(menu);

        // Output the selected option
        AnsiConsole.MarkupLine($"[yellow]STarte:[/] {selectedOption}");
    }
}
