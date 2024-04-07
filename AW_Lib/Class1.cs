namespace AW_Lib

{
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
}

    


