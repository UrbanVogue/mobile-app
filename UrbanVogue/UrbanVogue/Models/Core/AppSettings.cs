namespace UrbanVogue.Models.Core
{
    public class AppSettings
    {
        public const string API_URI = "http://192.168.0.108:8181/api/v1/";

        public string Token { get; set; }

        public int RequestTimeOut { get; set; } = 10;

        public int RequestRetry { get; set; } = 3;

        public const int StandardRequestTime = 12;

    }
}
