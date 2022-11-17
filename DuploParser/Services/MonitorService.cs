using DuploParser.Abstractions;
using LiteDB;
using System.Threading;

namespace DuploParser.Services
{
    public class MonitorService : BackgroundService
    {
        private readonly IDuploApi _api;

        private readonly IServiceProvider _serviceProvider;

        private readonly LiteDatabase _liteDB;

        public MonitorService(IDuploApi api, IServiceProvider provider)
        {
            _api = api;
            _serviceProvider = provider;
            _liteDB = provider.GetRequiredService<LiteDatabase>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int offset = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var tyres = _api.GetTyresAsync(offset);

                }
                catch (Exception ex)
                {
                    
                }
                await Task.Delay(5000);
            }
        }


    }
}
