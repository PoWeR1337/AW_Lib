using AW_Lib;
using Musik;
using Sad;
using Spectre.Console;

namespace ConsoleApp
{
    internal class ProgramBase
    {


        // Moderner Titelbereich mit Panel und Grid
        public static void TitelModern(string ip)
        {
            IAppInfo appInfo = new AppInfo
            {
                Title = "AWET",
                Version = "0.2",
                currentDate = DateTime.Now
            };

            // Header mit Farbverlauf
            var header = new FigletText(appInfo.Title)
                .Centered()
                .Color(Color.Fuchsia);

            // Info-Grid
            var grid = new Grid();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddRow("[yellow]Benutzer:[/]", $"[bold]{Environment.UserName}[/]");
            grid.AddRow("[green]Status:[/]", "[bold]Bereit[/]");
            grid.AddRow("[blue]Umgebung:[/]", "[bold]Produktiv[/]");
            grid.AddRow("[red]IP:[/]", $"[bold]{ip}[/]");
            grid.AddRow("[grey]Datum:[/]", $"[bold]{appInfo.currentDate:dd.MM.yyyy HH:mm}[/]");

            var infoPanel = new Panel(grid)
                .Border(BoxBorder.Rounded)
                .Header("[bold]Info[/]")
                .Padding(1, 1)
                .Expand();

            // Version als Rule
            var versionRule = new Rule($"[bold red]Version {appInfo.Version}[/]").Centered();

            // Rendern
            AnsiConsole.Write(header);
            AnsiConsole.Write(versionRule);
            AnsiConsole.Write(infoPanel);
            AnsiConsole.WriteLine();
        }

        // Modernes Menü mit Tabelle und Emojis
        static bool H_Menu()
        {
            // Modultabelle
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Centered()
                .Title("[bold underline green]Module Übersicht[/]");

            table.AddColumn(new TableColumn("[bold yellow]Modul[/]").Centered());
            table.AddColumn(new TableColumn("[bold green]Beschreibung[/]").Centered());
            table.AddColumn(new TableColumn("[bold blue]Symbol[/]").Centered());

            table.AddRow("[bold]Musik[/]", "Musikfunktionen & Analyse", "[yellow]🎵[/]");
            table.AddRow("[bold]Krypto[/]", "Kryptografie-Tools", "[green]🪙[/]");
            table.AddRow("[bold]Telegram[/]", "Telegram Bot Steuerung", "[blue]💬[/]");
            table.AddRow("[bold]Beenden[/]", "Programm verlassen", "[red]⏻[/]");

            AnsiConsole.Write(table);

            // Auswahlmenü mit Highlight
            var menu = new SelectionPrompt<string>()
                .Title("[bold blue]Bitte wähle ein Modul:[/]")
                .PageSize(6)
                .HighlightStyle(new Style(foreground: Color.Fuchsia, decoration: Decoration.Bold))
                .AddChoices(new[] { "Musik", "Krypto", "Telegram", "Beenden" });

            var selectedOption = AnsiConsole.Prompt(menu);

            AnsiConsole.MarkupLine($"[yellow]Starte:[/] [bold]{selectedOption}[/]");

            switch (selectedOption)
            {
                case "Telegram":
                    Console.Clear();
                    var header = new FigletText("TeleGram Bot")
                        .Centered()
                        .Color(Color.Blue);
                    AnsiConsole.Write(header);
                    CTelegegram.TelegramConsole.Konstruktor();
                    return true;
                case "Krypto":
                    Console.Clear();
                    AnsiConsole.MarkupLine("[green]Krypto-Modul folgt...[/]");
                    AnsiConsole.MarkupLine("[grey]Drücke eine Taste, um zurückzukehren.[/]");
                    Console.ReadKey();
                    return true;
                case "Musik":
                    Console.Clear();
                    MusikConsole.konstrukt();
                    return true;
                case "Beenden":
                    AnsiConsole.MarkupLine("[red]Beende Anwendung...[/]");
                    return false;
                default:
                    return true;
            }
        }

        static void MainMenuKonstruktor()
        {
            while (true)
            {
                Console.Clear();
                TitelModern(A_IP.GetPublicIpAddress());
                if (!H_Menu())
                    break;
            }
        }

        private static void RegisterLoginMenu()
        {
          //  throw new NotImplementedException();
          Console.WriteLine ("RegisterLoginMenu is not implemented yet.");
        }
    }
}