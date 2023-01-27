using System;
using System.Threading.Tasks;
using CarApi.Model;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace CarApi.Core.Services
{
    public interface IAutoPliusService
    {
        Task<string> GetNewAdListPage(int page);
        Task<string> GetAllCarAdsByYear(string model, int page, int yearFrom, int yearTo, int toAmount);
    }

    public class AutoPliusService : IAutoPliusService
    {
        private readonly AppSettings _appSettings;
        public AutoPliusService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public async Task<string> GetNewAdListPage(int page)
        {
            using var driver = GetChromeDriver();
            var url = $"https://autoplius.lt/skelbimai/naudoti-automobiliai?category_id=2&has_damaged_id=10924&older_not=2&steering_wheel_id=10922&sell_price_from=1500&sell_price_to=5000&slist=1713959140&make_date_from=2008&qt=&qt_autocomplete=&page_nr={page}";

            driver.Navigate().GoToUrl(url);
            var acceptButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            if(acceptButton != null)
                acceptButton.Click();

            return driver.PageSource;
        }

        public async Task<string> GetAllCarAdsByYear(string model,int page, int yearFrom, int yearTo,int toAmount)
        {
            using var driver = GetChromeDriver();

            var url = $"https://autoplius.lt/skelbimai/naudoti-automobiliai?category_id=2&has_damaged_id=10924&make_date_from={yearFrom}&make_date_to={yearTo}&make_id{model}&sell_price_to={toAmount}&steering_wheel_id=10922&offer_type=0&qt=&qt_autocomplete=&page_nr={page}";

            driver.Navigate().GoToUrl(url);
            var acceptButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            if (acceptButton != null)
                acceptButton.Click();

            return driver.PageSource;
        }

        private RemoteWebDriver GetChromeDriver()
        {
            //return new ChromeDriver(@"C:\WebDriver");
            var seleniumUrl = $"{_appSettings.SeleniumUrl}";
            var chromeOptions = new ChromeOptions();
            return new RemoteWebDriver(new Uri(seleniumUrl), chromeOptions);
        }
    }
}
