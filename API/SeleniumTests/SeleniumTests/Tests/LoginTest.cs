using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Tests
{
    class LoginTest
    {

        //Browser Driver
        IWebDriver webDriver = new ChromeDriver();

        [SetUp]
        public void Setup()
        {
           
            //Navigate to site 
            webDriver.Navigate().GoToUrl("https://localhost:44342/");

        }

        [Test]
        public void Login()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.ClickLogin();

            //-----Disconnect--------

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.Login("testQwerty", "12345Qwerty.");

            var loginComSucesso = webDriver.FindElement(By.XPath("/html/body/div/main/div/div/h3"));

            var sucesso = false;

            var loginMessage = loginComSucesso.Text;

            if (loginMessage == "Login efectuado com sucesso")
            {
                sucesso = true;
                Console.WriteLine("Login bem sucedido!");
            }

            Assert.That(sucesso, Is.True);

            //Assert.Pass();
        }

        [TearDown]
        public void TearDown() => webDriver.Quit();
    }
}
