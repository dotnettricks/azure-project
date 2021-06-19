using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            //https://codewithmukesh.com/blog/serilog-in-aspnet-core-3-1/
            //Install - Package Serilog.AspNetCore
            //Install - Package Serilog.Settings.Configuration
            //Install - Package Serilog.Enrichers.Environment
            //Install - Package Serilog.Enrichers.Process
            //Install - Package Serilog.Enrichers.Thread
            //Install - Package Serilog.Sinks.MSSqlServer

            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            try
            {

                Log.Information("Application Starting.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() //Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
