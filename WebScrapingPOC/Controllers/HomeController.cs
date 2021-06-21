using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebScrapingPOC.Models;
using System.Web;
using Newtonsoft.Json.Linq;
using WebScrapingPOC.Utility;
using System.Xml.Xsl;

namespace WebScrapingPOC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public  ActionResult Index()
        {
            var res = GetDataFromViewPage();
            return Content(res,"text/html");
            
        }

        private string GetDataFromViewPage()
        {
            var bodyHtml = HtmlUtility.GetHtmlStringFromURL(@"https://www.maseraticf.com/used-vehicles/");
            return ParseHtml (bodyHtml);
        }

        private string ParseHtml(string htmlData)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlData);

            var totalCount = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='lvrp-results-container']//div[contains(@class,'results-title')]//h1");
            var vehicles = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class ,'used-vehicle')]");

            var vehicleSearchResult = new VehicleSearchResult();
            vehicleSearchResult.TotalCount = Convert.ToInt32(totalCount.InnerText.Split(' ')[0]);

            foreach (var vehicle in vehicles)
            {
                var data = JObject.Parse(HttpUtility.HtmlDecode(vehicle.GetDataAttribute("vehicle").Value));
                VehicleDetail vehicleDetail = GetVehicleInfo(vehicle, data);
                vehicleSearchResult.Vehicles.Add(vehicleDetail);
            }

            return ObjectToHtml(vehicleSearchResult);

        }

        private static string ObjectToHtml(VehicleSearchResult vehicleSearchResult)
        {
            var xmlData = XMLUtility.ObjectToXML(vehicleSearchResult);

            System.IO.File.WriteAllText(@"D:\POC\temp\MyTest.xml", xmlData);

            var myXslTrans = new XslCompiledTransform();
            myXslTrans.Load("./SearchResultVehicle.xsl");

            myXslTrans.Transform(@"D:\POC\temp\MyTest.xml", @"D:\POC\temp\result.html");
            return System.IO.File.ReadAllText(@"D:\POC\temp\result.html");
        }

        private static VehicleDetail GetVehicleInfo(HtmlNode vehicle, JObject data)
        {
            return new VehicleDetail
            {
                Name = vehicle.SelectSingleNode("div[contains(@class,'hit')]/div[contains(@class,'hit-content')]/div[contains(@class,'hit-content-title-wrap')]/a[@class='hit-link']/h2[contains(@class,'result-title')]").InnerText,
                Price = vehicle.SelectSingleNode("div[contains(@class,'hit')]/div[contains(@class,'hit-content')]/div[contains(@class,'hit-content-title-wrap')]/div[contains(@class,'result-price')]/div[contains(@class,'price--primary')]/div[contains(@class,'price__value')]").InnerText,
                Exterior = vehicle.SelectSingleNode("div[contains(@class,'hit')]/div[contains(@class,'hit-content')]/div[contains(@class,'hit-content-title-wrap')]/ul/li[contains(@class,'exterior')]")?.InnerText.Split(':')[1],
                Interior = vehicle.SelectSingleNode("div[contains(@class,'hit')]/div[contains(@class,'hit-content')]/div[contains(@class,'hit-content-title-wrap')]/ul/li[contains(@class,'interior')]")?.InnerText.Split(':')[1],
                Mileage = vehicle.SelectSingleNode("div[contains(@class,'hit')]/div[contains(@class,'hit-content')]/div[contains(@class,'hit-content-title-wrap')]/ul/li[contains(@class,'mileage')]")?.InnerText.Split(':')[1],
                Stock = data["stock"]?.Value<string>(),
                Year = data["year"]?.Value<string>(),
                Make = data["make"]?.Value<string>(),
                Model = data["model"]?.Value<string>(),
                BodyStyle = data["bodystyle"]?.Value<string>(),
                Type = data["type"]?.Value<string>(),
                Vin = data["vin"]?.Value<string>()
            };
        }
    }
}
