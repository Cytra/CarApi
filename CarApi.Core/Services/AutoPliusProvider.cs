using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarApi.Model;
using HtmlAgilityPack;

namespace CarApi.Core.Services
{
    public interface IAutoPliusProvider
    {
        Task<List<CarAd>> Test();
    }
    public class AutoPliusProvider : IAutoPliusProvider
    {
        private readonly string[] _charsToRemove = new [] { " ", "&", "e", "e", "r", "o", "u", ";", "k", "m" };
        private readonly IAutoPliusService _autoPliusService;
        public AutoPliusProvider(IAutoPliusService autoPliusService)
        {
            _autoPliusService = autoPliusService;
        }

        public async Task<List<CarAd>> Test()
        {
            var result = new List<CarAd>();
            var page = 1;
            var carListHtml = await _autoPliusService.GetAllCarAdsByYear(page, 2009, 2015);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(carListHtml);

            GetAllAdsFromPage(htmlDoc, result);

            var paging = htmlDoc.DocumentNode.Descendants("div").Single(node => node.GetAttributeValue("class", "").Contains("paging")).InnerText.Trim();
            var splitPaging = paging.Split('/');
            var end = int.Parse(splitPaging[1]);
            for (int i = 2; i <= end; i++)
            {
                var html = await _autoPliusService.GetAllCarAdsByYear(i, 2009, 2015);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                GetAllAdsFromPage(htmlDoc, result);
            }

            return result;
        }

        private List<CarAd> GetAllAdsFromPage(HtmlDocument htmlDoc, List<CarAd> result)
        {
            var table = htmlDoc.DocumentNode.Descendants("div").SingleOrDefault(node => node.GetAttributeValue("class", "").Contains("list-items"));
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
            var description = node.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("description"));
            var heading = description.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("heading"));
            var titleContainer = heading.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("title-container"));
            result.Year = ConvertToInt(titleContainer.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("title-year")).InnerText.Trim());
            result.Name = titleContainer.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("title-content")).InnerText.Trim();
            var priceString = heading.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("pricing-container")).InnerText.Trim();
            result.Price = ConvertToInt(priceString);
            var parameters = description.ChildNodes.Single(x => x.GetAttributeValue("class", "").Contains("item-parameters"))
                .ChildNodes.Where(x => x.Name == "span").ToList();

            result.GasType = parameters[0].InnerText.Trim();
            result.Power = parameters[1].InnerText.Trim();
            result.GearBox = parameters[2].InnerText.Trim();
            if (parameters.Count == 6)
            {
                result.Mileage = ConvertToInt(parameters[3].InnerText);
                result.CarType = parameters[4].InnerText.Trim();
                result.City = parameters[5].InnerText.Trim();
            }
            else
            {
                result.CarType = parameters[3].InnerText.Trim();
                result.City = parameters[4].InnerText.Trim();
            }

            return result;
        }

        private int ConvertToInt(string input)
        {
            foreach (var c in _charsToRemove)
            {
                input = input.Replace(c, string.Empty);
            }

            return int.Parse(input);
        }
    }
}
