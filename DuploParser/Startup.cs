using DuploParser.Abstractions;
using DuploParser.Services;
using LiteDB;

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
            services.AddScoped<LiteDatabase>(db => new LiteDatabase("duplo.db"));
            services.AddSingleton<IDuploApi, DuploApi>(api => new DuploApi(Configuration["BearerToken"]));
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
