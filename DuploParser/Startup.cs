using DuploParser.Abstractions;
using DuploParser.Data;
using DuploParser.Services;
using Microsoft.EntityFrameworkCore;

namespace DuploParser
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStr = Configuration["DUPLO_CONNECTION"] ?? throw new InvalidOperationException("connection string not found");
            var bearerToken = Configuration["BEARER_TOKEN"] ?? throw new InvalidOperationException("bearer token not found");
            var botToken = Configuration["BOT_TOKEN"] ?? throw new InvalidOperationException("bot token not found");
            var channelId = Configuration["CHANNEL_ID"] ?? throw new InvalidOperationException("channel id not found");
            Console.WriteLine($"Connection string: {connectionStr}");
            Console.WriteLine($"Bearer: {bearerToken}");
            Console.WriteLine($"Bot: {botToken}");
            Console.WriteLine($"Channel ID: {channelId}");

            services.AddDbContext<AppDb>(options => options.UseMySql(connectionStr, new MySqlServerVersion(new Version(8, 0, 27))));
            services.AddSingleton<IDuploApi, DuploApi>(service => new DuploApi(bearerToken));

            services.AddSingleton<ITelegramProvider, TelegramProvider>(service => new TelegramProvider(channelId));
            services.AddHostedService(service => new TelegramService(botToken, service.GetRequiredService<ITelegramProvider>()));
            services.AddHostedService<MonitorService>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallback(async context =>
                {
                    context.Response.Redirect("/");
                });
            });
        }
    }
}
