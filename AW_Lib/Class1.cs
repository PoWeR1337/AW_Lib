namespace AW_Lib

    /// AW Libary 
   ///Update : 7.4.24
   ///ToDo :
   ///1 APP Info -> Logging, Version
   ///2.Telegram bot implentierung
   ///3.  

{

     /// <summary>
    /// APP Info
    /// </summary>
    public interface IAppInfo
        {
            string Title { get; set; }
            string Version { get; set; }
        }

        public class AppInfo : IAppInfo
        {
        public string Title { get; set; } = "AW-E";
        public string Version { get; set; } = "0.0";
        }
    // ENDE INFO


    /// <summary>
    /// Telegram
    /// 
    /// </summary>
    class A_Bot
    {
        
    }

}

    


