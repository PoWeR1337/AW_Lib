﻿using AW_Lib;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBack;

namespace ConsoleApp.tools.local
{
    internal class TelegramConsole
    {
        public static void Konstruktor()
        {
            TeleHauptmenu();
        }




        private static void TeleHauptmenu()
        {

            // new APP Info Interface
            ITelegram appInfo = new Tele();

            appInfo.Title = "Telegram Bot";
            appInfo.Version = "0.1";

            // Header with AWET
            var header = new FigletText(appInfo.Title)
                .Centered()
                .Color(Color.Blue);

            // APP Version separator
            var versionSeparator = new Rule($"[red]{appInfo.Version}[/]")
                .Centered();

            // Date
            appInfo.currentDate = DateTime.Now;
            // APP Version separator
            var Bot_Tok = new Rule($"[red]{appInfo.Tooken}[/]")
                .Centered();


            // Render the interface
            AnsiConsole.Write(header);
            AnsiConsole.Write(versionSeparator);
            AnsiConsole.Write(Bot_Tok);

            // Boot Tooken Shhicken 

            // nur wenn kein tooken da ist soll :

            appInfo.Tooken = Console.ReadLine();

            var Bot_Tok1 = new Rule($"[red]{appInfo.Tooken}[/]")
             .Centered();

            AnsiConsole.Write(Bot_Tok1);
            Console.ReadKey();

        }



    }
}
