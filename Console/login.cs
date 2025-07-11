using Spectre.Console;
using AW_Lib;
using Musik;
using System.Collections.Generic;
    using System;


namespace Sad
{
   
    class Safe
    {
        // Einfache Userdatenbank (Username, Passwort)
        static Dictionary<string, string> users = new();

        
       


       

        public static void RegisterLoginMenu()
        {
            while (true)
            {
                Console.Clear();
                var menu = new SelectionPrompt<string>()
                    .Title("[bold blue]Willkommen! Bitte wählen:[/]")
                    .AddChoices("Login", "Registrieren", "Beenden");

                var choice = AnsiConsole.Prompt(menu);

                switch (choice)
                {
                    case "Login":
                        if (Login())
                            return; // Erfolgreich eingeloggt, weiter zum Hauptmenü
                        break;
                    case "Registrieren":
                        Register();
                        break;
                    case "Beenden":
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static bool Login()
        {
            var username = AnsiConsole.Ask<string>("[yellow]Benutzername:[/]");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Passwort:[/]").PromptStyle("red").Secret());

            if (users.TryGetValue(username, out var storedPw) && storedPw == password)
            {
                AnsiConsole.MarkupLine("[green]Login erfolgreich![/]");
                AnsiConsole.MarkupLine("[grey]Drücke eine Taste, um fortzufahren...[/]");
                Console.ReadKey();
                return true;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Login fehlgeschlagen![/]");
                AnsiConsole.MarkupLine("[grey]Drücke eine Taste, um es erneut zu versuchen...[/]");
                Console.ReadKey();
                return false;
            }
        }

        static void Register()
        {
            var username = AnsiConsole.Ask<string>("[yellow]Neuer Benutzername:[/]");
            if (users.ContainsKey(username))
            {
                AnsiConsole.MarkupLine("[red]Benutzername existiert bereits![/]");
                AnsiConsole.MarkupLine("[grey]Drücke eine Taste, um zurückzukehren...[/]");
                Console.ReadKey();
                return;
            }
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Passwort wählen:[/]").PromptStyle("red").Secret());
            users[username] = password;
            AnsiConsole.MarkupLine("[green]Registrierung erfolgreich![/]");
            AnsiConsole.MarkupLine("[grey]Drücke eine Taste, um fortzufahren...[/]");
            Console.ReadKey();
        }

        // ... (restlicher Code wie bisher, z.B. MainMenuKonstruktor, TitelModern, H_Menu)
    }
}