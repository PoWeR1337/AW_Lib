using System;
using Telegram.Bot;

class Program
{
    private static string botToken = "YOUR_BOT_TOKEN";
    private static long chatId = YOUR_CHAT_ID; // Setzen Sie die Chat-ID für den gewünschten Chat ein

    static async Task Main(string[] args)
    {
        var botClient = new TelegramBotClient(botToken);

        // Nachricht senden
        await botClient.SendTextMessageAsync(chatId, "Hello, World!");

        // Token wechseln und Nachricht erneut senden
        botToken = "YOUR_NEW_BOT_TOKEN"; // Ändern Sie den Token hier
        botClient = new TelegramBotClient(botToken);
        await botClient.SendTextMessageAsync(chatId, "Hello, World with new token!");

        // Anpassung der Nachrichten je nach Bedarf
        string customMessage = "Custom message here!";
        await botClient.SendTextMessageAsync(chatId, customMessage);
    }
}