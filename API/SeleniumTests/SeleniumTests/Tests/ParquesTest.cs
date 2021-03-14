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
    class ParquesTest
    {


        //Browser Driver
        IWebDriver webDriver = new ChromeDriver();

        [SetUp]
        public void Setup()
        {

            //Navigate to site 
            webDriver.Navigate().GoToUrl("https://pseudocompany.azurewebsites.net/");

        }

        [Test]
        public void Parques()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.ClickLogin();

            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.Login("pseudocompanygrupo3@gmail.com", "12345Qwerty.");

            homePage.ClickParques();

            //-----Disconnect--------

            ParquesPage parquesPage = new ParquesPage(webDriver);
            parquesPage.Submeter("4428-108 Porto Praça da Vida 83 Aparkai 2");

            ParquesDetailsPage parquesDetailsPage = new ParquesDetailsPage(webDriver);

            parquesDetailsPage.SubmeterDataInicio("13040020211800");
            
            parquesDetailsPage.SubmeterDataFim("13040020212000");
            
            var reservaComSucesso = webDriver.FindElement(By.XPath("/html/body/div/main/div/div/h3"));

            var sucesso = false;

            var reservaMessage = reservaComSucesso.Text;

            if (reservaMessage == "Reserva efetuada com sucesso")
            {
                sucesso = true;
                Console.WriteLine("Reserva bem sucedida!");
            }

            Assert.That(sucesso, Is.True);

            //Assert.Pass();
        }

        [TearDown]
        public void TearDown() => webDriver.Quit();
    }
}
