using AW_Lib;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TelegramBack;

namespace Musik
{
    internal class MusikConsole
    {



        public static void konstrukt()
        {
            Start("Musik");
            Path();
            MusikHauptmenu();
        }



        static void Start(string title)
        {
            // new APP Info Interface
            IAppInfo appInfo = new AppInfo();

            appInfo.Title = "Musik";
            appInfo.Version = "0.1";
            appInfo.Title = title;


            // Header with AWET
            var header = new FigletText(appInfo.Title)
                .Centered()
                .Color(Color.Blue);
            // APP Version separator
            var versionSeparator = new Rule($"[red]{appInfo.Version}[/]")
                .Centered();
            // Date
            appInfo.currentDate = DateTime.Now;
            // Render the interface
            AnsiConsole.Write(header);
            AnsiConsole.Write(versionSeparator);


        }

        // Musik Lokal und URL Return von path

        static void Path()
        {
            var pfad = "Leer";
            // Create a menu
            var menu = new SelectionPrompt<string>()
                .Title("[red]Menü:[/]")
                .PageSize(3)
                .AddChoices(new[] { "Local Path", "URL", });

            var selectedOption = AnsiConsole.Prompt(menu);

            switch(selectedOption)
            {
                case "Local Path":
                    pfad = AnsiConsole.Ask<string>("Kompletter [green]Pfad[/]!");
                    
                    break;
                case "URL":
                    pfad = AnsiConsole.Ask<string>("Komplette [red]URL[/]");
                    break;

                default:
                    Console.Clear();
                    Path();
                    break;
            }
        
        }
        static void MusikHauptmenu()
        {
            IAppInfo appInfo = new AppInfo();

            // Create a menu
            var menu = new SelectionPrompt<string>()
                .Title("[red]Menü:[/]")
                .PageSize(3)
                .AddChoices(new[] { "Analyzer", "Downloader", "Converter" });


            var selectedOption = AnsiConsole.Prompt(menu);

            // Output the selected option
            AnsiConsole.MarkupLine($"[red]Starte:[/] {selectedOption}");

            switch (selectedOption)
            {
                // AN
                case "Analyzer":
                    Console.Clear();
                    Start("Analyzer");
                    break;
                case "Downloader":
                    Console.Clear();
                    Start("Donwloader");
                    break;
                case "Converter":
                    Console.Clear();
                    Start("Converter");
                    break;
                default:
                    Console.Clear();
                    MusikHauptmenu();
                    break;
            }
        }
    }
}

