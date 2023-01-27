using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace UnitTests

{

    public class SeleniumTests

    {
        private RemoteWebDriver selenium;

        private StringBuilder verificationErrors;


        public SeleniumTests()

        {
            var seleniumUrl = $"http://172.17.0.2:4444";
            var chromeOptions = new ChromeOptions()
            {
                BrowserVersion = "108.0",
            };

            //return new RemoteWebDriver(new Uri(seleniumUrl), chromeOptions);
            //selenium = new RemoteWebDriver("localhost", 4444, "*iehta",
            //"http://www.google.com/");

            //selenium = new RemoteWebDriver(new Uri(seleniumUrl));

            //selenium.Navigate().GoToUrl("http://www.google.com/");

            verificationErrors = new StringBuilder();
        }

        //public void TeardownTest()
        //{
        //    try
        //    {
        //        selenium.Stop();
        //    }

        //    catch (Exception)
        //    {
        //        // Ignore errors if unable to close the browser
        //    }

        //    Assert.Equal("", verificationErrors.ToString());
        //}
        [Fact]

        public void TheNewTest()
        {
            // Open Google search engine.        
            selenium.Navigate().GoToUrl("http://www.google.com/");

            // Assert Title of page.
            Assert.Equal("Google", selenium.Title);

            // Provide search term as "Selenium OpenQA"
            //selenium.Type("q", "Selenium OpenQA");

            // Read the keyed search term and assert it.
            //Assert.Equal("Selenium OpenQA", selenium.GetValue("q"));

            // Click on Search button.
            //selenium.Click("btnG");

            //// Wait for page to load.
            //selenium.WaitForPageToLoad("5000");

            // Assert that "www.openqa.org" is available in search results.
            //Assert.True(selenium.IsTextPresent("www.openqa.org"));

            // Assert that page title is - "Selenium OpenQA - Google Search"
            Assert.Equal("Selenium OpenQA - Google Search",
                         selenium.Title);
        }
    }
}