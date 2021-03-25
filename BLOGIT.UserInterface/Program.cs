using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace BLOGIT.UserInterface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //The webhost is configured to log errors to different end points
                    webBuilder.ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("logging"));
                        logging.AddConsole(); //writes to the browser console
                        logging.AddDebug(); //writes to a hosting service cli like Heroku
                        logging.AddEventSourceLogger(); //writes to a cross-platform event source with the name Microsoft-Extensions-Logging
                        logging.AddNLog(); //writes to a log file
                    }).UseStartup<Startup>();
                });
    }
}
