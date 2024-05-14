using AW_Lib;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Musik Lokal 

        static void Path()
        {
            bool running = true;
            while (running)
            {
                // Display menu options
                var menu = new SelectionPrompt<string>()
                           .Title("Pfad")
                        .PageSize(3)
                        .AddChoices("Input file path", "Input URL", "Exit");


                var selectedOption = AnsiConsole.Prompt(menu);

                AnsiConsole.MarkupLine($"[red]Starte:[/] {selectedOption}");

                // Process user choice
                switch (selectedOption)
                {
                    case "input file path":
                        string filePath = GetFilePath();
                        AnsiConsole.MarkupLine($"File path entered: [yellow]{filePath}[/]");
                        break;
                    case "input url":
                        string url = GetUrl();
                        AnsiConsole.MarkupLine($"URL entered: [yellow]{url}[/]");
                        break;
                    case "exit":
                        running = false;
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Invalid choice. Please enter a valid option.[/]");
                        break;
                }
                AnsiConsole.WriteLine(); // Add a blank line for better readability
            }
        }

        static string GetFilePath()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("Enter file path")
                    .PromptStyle("grey")
                    .Validate(path =>
                    {
                        if (string.IsNullOrWhiteSpace(path))
                        {
                            return ValidationResult.Error("File path cannot be empty.");
                        }
                        else if (!System.IO.File.Exists(path))
                        {
                            return ValidationResult.Error("File path does not exist.");
                        }
                        return ValidationResult.Success();
                    })
            );
        }

        static string GetUrl()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("Enter URL")
                    .PromptStyle("grey")
                    .Validate(url => !string.IsNullOrWhiteSpace(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute) ? ValidationResult.Success() : ValidationResult.Error("URL is invalid."))
            );
        }
    

        static bool Location()
        {

            if (!AnsiConsole.Confirm("Lokal oder URL ? [y] Local [n] URL"))
            {
                AnsiConsole.MarkupLine("Ok... :(");
                return false;
            }

            return true;

        }



        static void MusikHauptmenu()
        {
            IAppInfo appInfo = new AppInfo();

            // Create a menu
            var menu = new SelectionPrompt<string>()
                .Title("[red]Menü:[/]")
                .PageSize(5)
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

