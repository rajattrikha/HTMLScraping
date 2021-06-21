using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebScrapingPOC.Utility
{
    public class HtmlUtility
    {
        public static string GetHtmlStringFromURL(string absoluteUrl)
        {
            
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            IWebDriver driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl(absoluteUrl);
            var htmlBody = By.TagName("body");
            var body = driver.FindElement(htmlBody);
            var bodyHtml = body.GetAttribute("innerHTML");
            return bodyHtml;
            
        }
    }
}
