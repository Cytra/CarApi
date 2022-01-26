using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CarApi.Core.Services
{
    public interface IAutoPliusService
    {
        Task<string> GetNewAdListPage(int page);
        Task<string> GetAllCarAdsByYear(int page, int yearFrom, int yearTo);
    }

    public class AutoPliusService : IAutoPliusService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private AppSettings _settings;
        public AutoPliusService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> settings)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings.Value;
        }

        public async Task<string> GetNewAdListPage(int page)
        {
            var client = _httpClientFactory.CreateClient("autoPlius");
            var url = $"/skelbimai/naudoti-automobiliai?category_id=2&older_not=-1&offer_type=0&qt=&qt_autocomplete=&page_nr={page}";
            var httpRequest = GetMessage(url);
            var response = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> GetAllCarAdsByYear(int page, int yearFrom, int yearTo)
        {
            var client = _httpClientFactory.CreateClient("autoPlius");
            var url = $"/skelbimai/naudoti-automobiliai?category_id=2&make_date_from={yearFrom}&make_date_to={yearTo}&make_id%5B92%5D=1184&offer_type=0&qt=&qt_autocomplete=&page_nr={page}";
            var httpRequest = GetMessage(url);
            var response = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        private HttpRequestMessage GetMessage(string url)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Add("cookie", _settings.AutopliusCookie);
            httpRequest.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
            return httpRequest;
        }
    }
}
