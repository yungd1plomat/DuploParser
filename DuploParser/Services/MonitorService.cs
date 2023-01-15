using DuploParser.Abstractions;
using DuploParser.Data;
using DuploParser.Models;
using DuploParser.Models.Api;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DuploParser.Services
{
    public class MonitorService : BackgroundService
    {
        private readonly IDuploApi _api;

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ILogger _logger;

        private readonly ITelegramProvider _telegramProvider;

        private IList<string> _allTyres { get; set; }

        public MonitorService(IDuploApi api, IServiceScopeFactory scopeFactory, ILogger<MonitorService> logger, ITelegramProvider telegramProvider)
        {
            _api = api;
            _scopeFactory = scopeFactory;
            _logger = logger;
            _telegramProvider = telegramProvider;
            _allTyres = new List<string>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Не рекомендуется инъекция scoped сервисов в singleton, поэтому будем сами управлять жизненным циклом бд
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDb>();
                int offset = 0;
                var localTyres = new List<string>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    var filters = await db.Filters.ToListAsync();
                    try
                    {
                        var tyres = await _api.GetTyresAsync(offset);
                        if (tyres is null || !tyres.Any())
                        {
                            var notAviables = _allTyres.Except(localTyres);
                            foreach (var tyre in notAviables)
                                _allTyres.Remove(tyre);
                            localTyres.Clear();
                            offset = 0;
                            continue;
                        }
                        var ids = tyres.Select(x => x.Id).ToList();
                        localTyres.AddRange(ids);
                        //_logger.LogInformation($"Processing page {offset}");
                        await ProcessTyres(tyres, filters);
                        offset++;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                    await Task.Delay(200);
                }
            }
        }

        private async Task ProcessTyres(IList<Tyre> tyres, IList<Filter> filters)
        {
            int count = 0;
            int skipped = 0;
            foreach (var tyre in tyres)
            {
                if (_allTyres.Contains(tyre.Id) ||
                    !filters.Any(filter => CompareFilter(tyre, filter)))
                {
                    skipped++;
                    continue;
                }
                _allTyres.Add(tyre.Id);
                _telegramProvider.Enqueue(tyre);
                count++;
            }
           // _logger.LogInformation($"Processed {tyres.Count} tyres, notified: {count}, skipped: {skipped}");
        }

        private bool CompareFilter(Tyre tyre, Filter filter)
        {
            return (string.IsNullOrEmpty(filter.Season) || filter.Season == tyre.Params.Season.Name) &&
                   (filter.Pins is null || filter.Pins == tyre.Params.Pins) &&
                   (filter.Width is null || filter.Width == tyre.Size.Width) &&
                   (filter.Profile is null || filter.Profile == tyre.Size.Profile) &&
                   (filter.Radius is null || filter.Radius == tyre.Size.Radius) &&
                   (filter.RunFlat is null || filter.RunFlat == tyre.Params.RunFlat) &&
                   (string.IsNullOrEmpty(filter.Code) || filter.Code == tyre.Sku);
        }
    }
}
