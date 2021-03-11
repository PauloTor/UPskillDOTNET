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
    class RegisterTest
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

            RegisterPage registerPage = new RegisterPage(webDriver);
            registerPage.Register("test2Qwerty2.", "qwerty2@mail.com", "12345Qwerty.");

            //TODO meter o path da mensagem de sucesso na criação de utilizador

            var RegistoComSucesso = webDriver.FindElement(By.XPath(""));

            var sucesso = false;

            var registerMessage = RegistoComSucesso.Text;

            if (registerMessage == "User created successfully!")
            {
                sucesso = true;
                Console.WriteLine("Registo bem sucedido!");
            }

            Assert.That(sucesso, Is.True);

        }
        [TearDown]
        public void TearDown() => webDriver.Quit();


    }
}
