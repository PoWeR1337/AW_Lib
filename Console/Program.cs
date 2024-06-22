using Spectre.Console;
using AW_Lib;
using MongoDB.Driver;
using MongoDB.Bson;

class Program
{
    static void Main(string[] args)
    {
        MainMenuKonstruktor();
        DB.Connect();
    }

    static void MainMenuKonstruktor()
    {
        Titel();
    }

    public static void Titel()
    {
        string IP;
        try
        {
            IP = A_IP.GetPublicIpAddress();
        }
        catch (Exception)
        {
            IP = "/";
        }

        AppInfo appInfo = new AppInfo();
        appInfo.Title = "AWET";
        appInfo.Version = 0.2;
        bool db = appInfo.DBConnect;
        string update = appInfo.Updated;
        string autor = appInfo.Author;
        double ping = appInfo.Ping;
        string On = db ? "Online" : "Offline";

        var header = new FigletText(appInfo.Title).Centered().Color(Color.Blue);
        var versionLine = new Rule($"Version: [red]{appInfo.Version}[/]").Centered();
        var ipLine = new Rule($"IP: [red]{IP}[/]").Centered();
        var dbLine = new Rule($"DB Status: [green]{On} | Ping: {ping}[/]").Centered();
        var autorLine = new Rule($"Author: [red]{autor}[/]").Centered();
        var updateLine = new Rule($"Last Updated: [red]{update}[/]").Centered();

        AnsiConsole.Render(header);
        AnsiConsole.Render(dbLine);
        AnsiConsole.Render(ipLine);
        AnsiConsole.Render(updateLine);
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
                    }
                },
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
                }
            };

            var databaseService = new DatabaseService("mongodb+srv://power:Joanna1337,,.@aw.71zfrso.mongodb.net/?retryWrites=true&w=majority&appName=aw", "aw");
            var toolsCollection = databaseService.GetCollection<Tool>("Tool");
            var tools = toolsCollection.Find(Builders<Tool>.Filter.Empty).ToList();

            // Merge tools and Offtools, avoiding duplicates
            foreach (var offTool in Offtools)
            {
                bool toolExists = tools.Any(t => t.Name == offTool.Name);
                if (!toolExists)
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

            AnsiConsole.Render(table);
        }
    }
}

public class Tool
{
    public string Name { get; set; }
    public List<Subtool> Subtools { get; set; }
}

public class Subtool
{
    public string Name { get; set; }
    public string Update { get; set; }
    public string Author { get; set; }
    public string GitHub { get; set; }
}

public class DatabaseService
{
    private readonly IMongoDatabase _database;

    public DatabaseService(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public bool Ping()
    {
        try
        {
            _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}

public class A_IP
{
    public static string GetPublicIpAddress()
    {
        return "192.168.1.1"; // Replace with actual IP retrieval logic
    }
}

public interface IAppInfo
{
    public string Title { get; set; }
    public bool DBConnect { get; set; }
    public string Updated { get; set; }
    public string Author { get; set; }
    public double Ping { get; set; }
}
