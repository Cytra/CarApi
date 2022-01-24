using System.Net.Http;
using System.Threading.Tasks;

namespace CarApi.Core.Services
{
    public interface IAutoPliusService
    {
        Task<string> GetNewAdListPage(int page);
    }

    public class AutoPliusService : IAutoPliusService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AutoPliusService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetNewAdListPage(int page)
        {
            //https://m.autoplius.lt/skelbimai/naudoti-automobiliai?category_id=2&older_not=-1&offer_type=0&qt=&qt_autocomplete=&page_nr=1
            //https://m.autoplius.lt/skelbimai/naudoti-automobiliai?category_id=2&older_not=-1&offer_type=0&qt=&qt_autocomplete=&page_nr=2
            var client = _httpClientFactory.CreateClient("autoPlius");
            var response = await client.GetAsync($"/skelbimai/naudoti-automobiliai?category_id=2&older_not=-1&offer_type=0&qt=&qt_autocomplete=&page_nr={page}");
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }
    }
}
