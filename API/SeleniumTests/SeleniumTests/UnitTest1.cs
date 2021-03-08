using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {

            //Browser Driver
            IWebDriver webDriver = new ChromeDriver();
            //Navigate to site 
            webDriver.Navigate().GoToUrl("https://localhost:44342/");



            Assert.Pass();
        }
    }
}