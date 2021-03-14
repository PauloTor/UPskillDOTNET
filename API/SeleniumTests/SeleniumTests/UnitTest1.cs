using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class Test1
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UnitTest()
        {

            //Browser Driver
            IWebDriver webDriver = new ChromeDriver();
            //Navigate to site 
            webDriver.Navigate().GoToUrl("https://pseudocompany.azurewebsites.net/");



            Assert.Pass();
        }
    }
}