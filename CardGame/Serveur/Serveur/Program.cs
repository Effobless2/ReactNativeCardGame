using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Serveur
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://192.168.1.62:5000")
                .Build();
    }
}
