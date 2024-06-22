using Spectre.Console;
using AW_Lib;
using MongoDB.Driver;
using static AW_Lib.Database;

class Program
{
    static void Main(string[] args)
    {  
        MainMenuKonstruktor();
    }

    static void MainMenuKonstruktor()
    {  
        
        Titel();
         DB.Connect();
        
    }

    public static void Titel()
    {
        IAppInfo appInfo = new AppInfo();
        string IP = A_IP.GetPublicIpAddress();
        bool db = appInfo.DBConnect;
        string update = appInfo.Updated;
        string autor = appInfo.Author;
        double ping = appInfo.Ping;
        string On = db ? "[green]Online[/]" : "[red]Offline[/]";

        var header = new FigletText(appInfo.Title).Centered().Color(Color.Blue);
        var versionSeparator = new Rule($"[red]{appInfo.Version}[/]").Centered();
        var IPs = new Rule($"[red]{IP}[/]").Centered();
        var DBC = new Rule(On).Centered();
        var AT = new Rule($"[red]{autor}[/]").Centered();
        var Separator = new Rule($"[red]{update}[/]").Centered();

        AnsiConsole.Write(header);
        AnsiConsole.Write(DBC);
        AnsiConsole.Write(IPs);
        AnsiConsole.Write(Separator);
    }
}

public class DB
{
    private static DatabaseService _dbService;

    public static void Connect()
    {
        _dbService = new DatabaseService("mongodb+srv://power:Joanna1337,,.@aw.71zfrso.mongodb.net/?retryWrites=true&w=majority&appName=aw", "aw");

        if (_dbService.Ping())
        {
            IAppInfo appInfo = new AppInfo();
            appInfo.DBConnect = true;
            appInfo.On = "Online";
            AnsiConsole.MarkupLine("[green]Database connection successful![/]");
            // Load tools from database
            var toolsCollection = _dbService.GetCollection<Tool>("Tool");
            var tools = toolsCollection.Find(tool => true).ToList();

            // Display tool menu
            var toolMenu = new SelectionPrompt<string>()
               .Title("Wähle  [green]Tool[/] für details : ")
               .AddChoices(tools.Select(t => t.Name).ToArray());

            var selectedToolName = AnsiConsole.Prompt(toolMenu);
            var selectedTool = tools.First(t => t.Name == selectedToolName);

            // Display subtool menu
            var subtoolMenu = new SelectionPrompt<string>()
                .Title($"Wähle [green]Subtool[/] aus [blue]{selectedTool.Name}[/] für details / start : ")
                .AddChoices(selectedTool.Subtools.Select(st => st.Name).ToArray());

            var selectedSubtoolName = AnsiConsole.Prompt(subtoolMenu);
            var selectedSubtool = selectedTool.Subtools.First(st => st.Name == selectedSubtoolName);

            // Display subtool details or start subtool
            var subtoolActionMenu = new SelectionPrompt<string>()
                .Title($"Program : [blue]{selectedSubtool.Name}[/]?")
                .AddChoices(new[] { "Details", "Start" });

            var subtoolAction = AnsiConsole.Prompt(subtoolActionMenu);

            switch (subtoolAction)
            {
                case "Details":
                    // Display subtool details
                    var table = new Table();
                    table.AddColumn("Property");
                    table.AddColumn("Value");

                    table.AddRow("Name", selectedSubtool.Name);
                    table.AddRow("Update", selectedSubtool.Update);
                    table.AddRow("Author", selectedSubtool.Author);
                    table.AddRow("GitHub", selectedSubtool.GitHub);

                    AnsiConsole.Write(table);
                    break;
                case "Start":
                    // Start the subtool
                    Console.WriteLine($"Starting [blue]{selectedSubtool.Name}[/]...");
                    // Add code to start the subtool here
                    break;
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Database connection failed![/]");
        }
    }
}