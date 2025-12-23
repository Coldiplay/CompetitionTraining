using API.DB;
using System.Net.Http;

namespace CompetitionTraining2.Model.Tools
{
    public static class Helper
    {
        public static HttpClient Client { get; private set; } = new HttpClient() { BaseAddress = new Uri(Config.ServerUrl) };

        public static User User { get; set; } = null!;
    }
}
