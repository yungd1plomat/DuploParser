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
            var bearerToken = Configuration["STEAM_API_KEY"] ?? throw new InvalidOperationException("bearer token not found");
            var botToken = Configuration["BOT_TOKEN"] ?? throw new InvalidOperationException("bot token not found");
            var channelId = Configuration["CHANNEL_ID"] ?? throw new InvalidOperationException("channel id not found");

            services.AddDbContext<AppDb>(options => options.UseMySql(connectionStr, new MySqlServerVersion(new Version(8, 0, 27))));
            services.AddSingleton<IDuploApi, DuploApi>(service => new DuploApi(bearerToken));
            services.AddSingleton<ITelegramService, TelegramService>(service => new TelegramService(botToken, channelId));
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
