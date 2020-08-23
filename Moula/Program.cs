using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog.Events;

namespace Moula
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetupLogging();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        private static void SetupLogging()
        {
            var xxx = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var jsonLoc = @Directory.GetCurrentDirectory() + @"\appsettings.json";
            IConfigurationRoot cfg = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(jsonLoc).Build();
            //IConfigurationRoot cfg = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).Build();

            var logFile = @Directory.GetCurrentDirectory() + cfg.GetValue<string>("LogFile");
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .WriteTo.File(logFile)
                .CreateLogger();

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
