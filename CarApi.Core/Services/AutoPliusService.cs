using System.Net.Http;
using System.Threading.Tasks;
using CarApi.Model;
using Microsoft.Extensions.Options;

namespace CarApi.Core.Services
{
    public interface IAutoPliusService
    {
        Task<string> GetNewAdListPage(int page, string cookie);
        Task<string> GetAllCarAdsByYear(string model, int page, int yearFrom, int yearTo, string cookie);
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

        public async Task<string> GetNewAdListPage(int page, string cookie)
        {
            var client = _httpClientFactory.CreateClient("autoPlius");
            // https://m.autoplius.lt/skelbimai/naudoti-automobiliai?category_id=2&has_damaged_id=10924&older_not=2&steering_wheel_id=10922&sell_price_from=1500&sell_price_to=5000&slist=1713959140&make_date_from=2008&qt=&qt_autocomplete=&page_nr=2
            var url = $"skelbimai/naudoti-automobiliai?category_id=2&has_damaged_id=10924&older_not=2&steering_wheel_id=10922&sell_price_from=1500&sell_price_to=5000&slist=1713959140&make_date_from=2008&qt=&qt_autocomplete=&page_nr={page}";
            var httpRequest = GetMessage(url,cookie);
            var response = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> GetAllCarAdsByYear(string model,int page, int yearFrom, int yearTo, string cookie)
        {
            var client = _httpClientFactory.CreateClient("autoPlius");
            var url = $"/skelbimai/naudoti-automobiliai?category_id=2&has_damaged_id=10924&make_date_from={yearFrom}&make_date_to={yearTo}&make_id{model}&steering_wheel_id=10922&offer_type=0&qt=&qt_autocomplete=&page_nr={page}";
            var httpRequest = GetMessage(url,cookie);
            var response = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        private HttpRequestMessage GetMessage(string url,string cookie)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Add("cookie", cookie);
            httpRequest.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
            return httpRequest;
        }
    }
}
