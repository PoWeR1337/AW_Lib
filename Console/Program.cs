using Spectre.Console;
using AW_Lib;
class Program
{
    static void Main(string[] args)
    {
        H_Menu();
    }

    

   public static void H_Menu()
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

        // Create a menu
        var menu = new SelectionPrompt<string>()
            .Title("[blue]Menü:[/]")
            .PageSize(5)
            .AddChoices(new[] { "Option 1", "Option 2", "Option 3" });

        // Render the interface
        AnsiConsole.Write(header);
        AnsiConsole.Write(versionSeparator);
        var selectedOption = AnsiConsole.Prompt(menu);

        // Output the selected option
        AnsiConsole.MarkupLine($"[yellow]STarte:[/] {selectedOption}");

    }
}
