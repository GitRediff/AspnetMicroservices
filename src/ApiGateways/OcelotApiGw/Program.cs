using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotApiGw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // Below for which ocelot version json file to pickup ---> ocelot.Development.json or ocelot.Local.json
                // this will add config file when application started [AddJSonFile()]
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile($"ocelot.{ hostingContext.HostingEnvironment.EnvironmentName }.json", true, true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                // below for logging on console and debug
                .ConfigureLogging((hostingContext, loggingBuilder) =>
                {
                    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));  
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug(); 
                });
    }
}
