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
            webDriver.Navigate().GoToUrl("https://pseudocompany.azurewebsites.net/");

        }

        [Test]
        public void Register()
        {
            HomePage homePage = new HomePage(webDriver);
            homePage.ClickRegister();

            //-----Disconnect--------

            RegisterPage registerPage = new RegisterPage(webDriver);
            registerPage.Register("SeleniumTest", "LastName", "Username", "pseudocompanygrupo3@gmail.com", "12345Qwerty.", "123456789", "cartao");

            //TODO meter o path da mensagem de sucesso na criação de utilizador

            var RegistoComSucesso = webDriver.FindElement(By.XPath("/html/body/div/main/div/p"));

            var sucesso = false;

            var registerMessage = RegistoComSucesso.Text;

            if (registerMessage == "Usuário nao autenticado!")
            {
                sucesso = true;
            }

            Assert.That(sucesso, Is.True);

        }
        [TearDown]
        public void TearDown() => webDriver.Quit();


    }
}
