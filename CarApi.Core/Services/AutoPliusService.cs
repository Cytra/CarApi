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
        private string _cookie =
            "__cf_bm=5hQgYYO7Xljw78jHy5XdHxpVrwmd3rlLaOaJJHUk2l8-1643062641-0-AfrqJJl3w2gsHRssoLOFOUi3AIqVf/taPvRqS1P4pYzwuuNDMpBi0BT2r2P5Nh7JCF0Zj5MsFM/WYz4AtwzANkQ=; PHPSESSID=b1av8037bogm1nf0f6235ujdjk; _ga=GA1.2.974645004.1643062689; _gid=GA1.2.569789813.1643062689; __gfp_64b=hZ7QGwlOt1olD.duupXlPfdQlfvWxbabnHu5nSJzfpr.x7|1643062688; OptanonAlertBoxClosed=2022-01-24T22:18:25.776Z; eupubconsent-v2=CPTV3hLPTV3hLAcABBENB_CsAP_AAH_AAChQIhtf_X__b3_j-_5_f_t0eY1P9_7_v-0zjhfdt-8N3f_X_L8X42M7vF36pq4KuR4Eu3LBIQVlHOHcTUmw6okVrzPsbk2cr7NKJ7PEmnMbO2dYGH9_n93TuZKY7______z_v-v_v____f_7-3_3__5_3---_e_V_99zLv9____39nP___9v-_9____giGASYal5AF2JY4Mm0aVQogRhWEh0AoAKKAYWiKwgdXBTsrgJ9QQsAEJqAjAiBBiCjBgEAAAEASERASAHggEQBEAgABACpAQgAI2AQWAFgYBAAKAaFiBFAEIEhBkcFRymBARItFBPZWIJQd7GmEIZZYAUCj-ioQEShBAsDISFg5jgCQEuFkgWYoXyAAA.f_gAD_gAAAAA; cf_chl_2=00fb4aa23f00e10; cf_chl_prog=x12; cf_clearance=F5QNDcpchBJeBAoXkialqwjHTOn3GLQGzL2gNGWfprw-1643062773-0-250; saved_searches=61ef2680e525b; _gat=1; OptanonConsent=isIABGlobal=false&datestamp=Tue+Jan+25+2022+00:24:22+GMT+0200+(Eastern+European+Standard+Time)&version=6.15.0&hosts=&consentId=a7cc1ed8-eae7-4adb-9650-5596e5d62994&interactionCount=1&landingPath=NotLandingPage&groups=C0001:1,C0003:1,C0004:1,STACK42:1&geolocation=LT;VL&AwaitingReconsent=false";
        private readonly IHttpClientFactory _httpClientFactory;
        public AutoPliusService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetNewAdListPage(int page)
        {
            //https://m.autoplius.lt/skelbimai/naudoti-automobiliai?category_id=2&older_not=-1&offer_type=0&qt=&qt_autocomplete=&page_nr=1
            //https://m.autoplius.lt/skelbimai/naudoti-automobiliai?category_id=2&older_not=-1&offer_type=0&qt=&qt_autocomplete=&page_nr=2
            //https://m.autoplius.lt/skelbimai/naudoti-automobiliai?category_id=2&older_not=-1&offer_type=0&qt=&qt_autocomplete=&__cf_chl_captcha_tk__=KLbq5yu82Up3Kb.0EOEr86BOu5GqFLDqZG_G5PmcUPI-1643062714-0-gaNycGzNDr0

            var client = _httpClientFactory.CreateClient("autoPlius");
            var url = $"/skelbimai/naudoti-automobiliai?category_id=2&older_not=-1&offer_type=0&qt=&qt_autocomplete=&page_nr={page}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Add("cookie", _cookie);
            httpRequest.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36");
            var response = await client.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead);
            var result = await response.Content.ReadAsStringAsync();
            return result;

        }
    }
}
