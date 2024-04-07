using AW_Lib;
using Telegram.Bot;


namespace TelegramBack
{
    
    public interface ITelegram
    {
        string Title { get; set; }
        string Version { get; set; }
        DateTime currentDate { get; set; }
        string Tooken {  get; set; }

    }

    public class Tele : ITelegram
    {
        public string Title { get; set; } = "AW-E";
        public string Version { get; set; } = "0.0";
        public DateTime currentDate { get; set; } = DateTime.Now;

        public string Tooken { get; set; } = "YOUR_ACCESS_TOKEN_HERE";
    }

    public class ConnectBot
    {
        ITelegram appInfo = new Tele();
       void Connect()
        {
         var botClient = new TelegramBotClient(appInfo.Tooken);

        }
      
    }
   
    }




