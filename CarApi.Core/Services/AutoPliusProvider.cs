using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarApi.Model;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace CarApi.Core.Services
{
    public interface IAutoPliusProvider
    {
        Task<List<CarAd>> GetAllNewAutoPliusCarAdds();
        Task<List<CarAd>> GetAllAutoPliusCarAdds(int yearFrom, int yearTo, CarModels carModel);
    }
    public class AutoPliusProvider : IAutoPliusProvider
    {
        private readonly string[] _charsToRemove = new [] { " ", "&", "e", "e", 
            "r", "o", "u", ";", "k", "m", "+", "s", "i", "a", "č", "€", "k", "W"};


        private readonly IAutoPliusService _autoPliusService;
        private readonly ILogger<AutoPliusProvider> _logger;
        public AutoPliusProvider(
            IAutoPliusService autoPliusService, 
            ILogger<AutoPliusProvider> logger)
        {
            _autoPliusService = autoPliusService;
            _logger = logger;
        }

        public async Task<List<CarAd>> GetAllNewAutoPliusCarAdds()
        {
            _logger.LogInformation("Started GetAllNewAutoPliusCarAdds");
            var result = new List<CarAd>();
            var page = 1;
            var carListHtml = await _autoPliusService.GetNewAdListPage(page);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(carListHtml);

            GetAllAdsFromPage(htmlDoc, result);

            var paging = htmlDoc.DocumentNode.Descendants("div").SingleOrDefault(node => node.GetAttributeValue("class", "").Contains("page-navigation-container"));
            if (paging == null)
            {
                return result;
            }

            var pagingUl = paging.ChildNodes.Single(x => x.Name == "ul");
            var pagingList = pagingUl.ChildNodes.Where(x => x.Name == "li")
                .Select(x => x.InnerText.Trim());

            var end = pagingList.Count() - 1;
            for (int i = 2; i <= end; i++)
            {
                var html = await _autoPliusService.GetNewAdListPage(i);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                GetAllAdsFromPage(doc, result);
            }

            return result;
        }

        private int GetPageNumber(HtmlDocument htmlDoc)
        {
            var paging = htmlDoc.DocumentNode.Descendants("div").SingleOrDefault(node => node.GetAttributeValue("class", "").Contains("page-navigation-container"));
            if (paging == null)
                return 0;
            var pagingUl = paging.ChildNodes.Single(x => x.Name == "ul");
            var pagingList = pagingUl.ChildNodes.Where(x => x.Name == "li")
                .Select(x => x.InnerText.Trim());

            var end = pagingList.Count() - 1;
            return end;
        }

        public async Task<List<CarAd>> GetAllAutoPliusCarAdds(int yearFrom, int yearTo, CarModels carModel)
        {
            _logger.LogInformation("Started GetAllAutoPliusCarAdds");
            var carId = CarEnumHelper.GetCarModelId(carModel);

            var result = new List<CarAd>();
            var page = 1;
            var carListHtml = await _autoPliusService.GetAllCarAdsByYear(carId, page, yearFrom, yearTo);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(carListHtml);

            GetAllAdsFromPage(htmlDoc, result);

            //var paging = htmlDoc.DocumentNode.Descendants("div").SingleOrDefault(node => node.GetAttributeValue("class", "").Contains("page-navigation-container"));
            //if (paging == null)
            //{
            //    return result;
            //}

            //var pagingUl = paging.ChildNodes.Single(x=> x.Name == "ul");
            //var pagingList = pagingUl.ChildNodes.Where(x => x.Name == "li")
            //    .Select(x=> x.InnerText.Trim());

            var end = GetPageNumber(htmlDoc);

            for (int i = 2; i <= end; i++)
            {
                var html = await _autoPliusService.GetAllCarAdsByYear(carId, i, yearFrom, yearTo);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                GetAllAdsFromPage(doc, result);
            }

            return result;
        }

        private List<CarAd> GetAllAdsFromPage(HtmlDocument htmlDoc, List<CarAd> result)
        {
            var table = htmlDoc.DocumentNode.Descendants("div").SingleOrDefault(node => node.GetAttributeValue("class", "").Contains("auto-lists lt"));
            if (table is null)
            {
                return null;
            }

            var callTrItems = table.ChildNodes.Where(x => x.Name == "a").ToList();
            foreach (var callTrItem in callTrItems)
            {
                result.Add(GetAd(callTrItem));
            }

            return result;
        }

        private CarAd GetAd(HtmlNode node)
        {
            var result = new CarAd();
            result.Link = node.Attributes["href"].Value;
            var description = node.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("announcement-body"));
            var heading = description.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("announcement-title"));
            result.Name = heading.InnerHtml.Trim();
            var priceElement = description.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("announcement-pricing-info"));
            result.Price = ConvertToInt(priceElement.ChildNodes.Single(x=> x.Name == "strong").InnerHtml.Trim());
            var paramDiv = description.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("announcement-parameters"));
            var parameters = paramDiv.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("bottom-aligner"))
                .ChildNodes.Where(x => x.Name == "span").ToList();

            result.Year = ConvertToInt(parameters[0].InnerText.Trim().Substring(0,4));
            result.GasType = parameters[1].InnerText.Trim();
            result.GearBox = parameters[2].InnerText.Trim();
            result.Power = parameters[3].InnerText.Trim();
            if (parameters.Count == 7)
            {
                result.Mileage = ConvertToInt(parameters[4].InnerText.Trim());
                result.City = parameters[5].InnerText.Trim();
                result.CarType = parameters[6].InnerText.Trim();
            }
            else
            {
                if(parameters.Count > 3)
                    result.CarType = parameters[3].InnerText.Trim();
                if (parameters.Count > 4)
                    result.City = parameters[4].InnerText.Trim();
            }

            return result;
        }

        private int ConvertToInt(string input)
        {
            try
            {
                foreach (var c in _charsToRemove)
                {
                    input = input.Replace(c, string.Empty);
                }

                return int.Parse(input);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Input string = {input}");
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
