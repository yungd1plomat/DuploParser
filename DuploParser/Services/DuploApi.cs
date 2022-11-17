using DuploParser.Abstractions;
using DuploParser.Models.Api;
using System.Net.Http.Headers;

namespace DuploParser.Services
{
    public class DuploApi : IDuploApi, IDisposable
    {
        const string BaseUrl = "https://duplo-api.shinservice.ru";

        public HttpClient _client { get; set; }

        public DuploApi(string token)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<IList<Tyre>?> GetTyresAsync(int page = 0)
        {
            return await _client.GetFromJsonAsync<List<Tyre>>($"{BaseUrl}/api/v1/tyre.json?page={page}");
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
