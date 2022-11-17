using DuploParser.Abstractions;
using DuploParser.Models;
using DuploParser.Models.Api;
using LiteDB;

namespace DuploParser.Services
{
    public class TelegramService : ITelegramService, IDisposable
    {
        const string BaseUrl = "https://api.telegram.org";


        private readonly string _token;

        private readonly string _channelId;

        private HttpClient _client;

        public TelegramService(string token, string channelId)
        {
            _token = token;
            _channelId = channelId;
            _client = new HttpClient();
        }

        public string GenerateDescription(Tyre tyre)
        {
            throw new NotImplementedException();
        }

        public async Task SendTyre(Tyre tyre)
        {
            var description = GenerateDescription(tyre);
            var photoUrl = tyre.Model.LogoUrl.Replace("//", null);
            var messageParams = new MessageParams()
            {
                ChatId = _channelId,
                Description = description,
                Photo = photoUrl,
            };
            await _client.PostAsJsonAsync<MessageParams>($"{BaseUrl}/bot{_token}/sendPhoto", messageParams);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
