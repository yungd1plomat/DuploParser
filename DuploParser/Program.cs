using DuploParser.Models;
using LiteDB;
using System.Diagnostics;
using System.Net.Http.Headers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DuploParser
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}