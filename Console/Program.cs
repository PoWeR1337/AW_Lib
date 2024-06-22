using Spectre.Console;
using AW_Lib;
using MongoDB.Driver;
using static AW_Lib.Database;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {  
        MainMenuKonstruktor();
    }

   public static void MainMenuKonstruktor()
    {
        AnsiConsole.Status()
    .AutoRefresh(false)
    .Spinner(Spinner.Known.Star)
    .SpinnerStyle(Style.Parse("green bold"))
    .Start("Laden...", ctx =>
    {

        try
        {
         Titel();
         DB.Connect();
        }
        catch(Exception ex)
        {
            Console.Clear();
            AnsiConsole.WriteException(ex);
            MainMenuKonstruktor();
        }
        // Omitted
        ctx.Refresh();
    });

    }

    public static void Titel()
    {
          
        IAppInfo appInfo = new AppInfo();

        try
        {
 
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
        catch(Exception Ex)
        {
            AnsiConsole.WriteException(Ex);
            Thread.Sleep(1000);
            Console.Clear();
            MainMenuKonstruktor();
        }
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
            AppInfo appInfo = new AppInfo();
            appInfo.DBConnect = true;
            appInfo.On = "Online";
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
                    Console.ReadLine();
                    Console.Clear();
                    Program p = new Program();
                    Program.MainMenuKonstruktor();
                    break;
                case "Start":
                    // Start the subtool
                    Console.WriteLine($"Starting [blue]{selectedSubtool.Name}[/]...");
                    // Execute the subtool
                    ExecuteSubtool(selectedSubtool);
                    break;
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Database connection failed![/]");
        }
    }
        private static void ExecuteSubtool(Subtool subtool)
    {
        // Execute the subtool here
        // For example, you can use Process.Start to execute a command
        Process.Start(new ProcessStartInfo
        {
            FileName = subtool.Name,
            Arguments = subtool.Update,
            WorkingDirectory = "" ,
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        });
    }
}
