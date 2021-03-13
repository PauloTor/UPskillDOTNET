using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    class HomePage
    {

        public HomePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        private IWebDriver Driver { get; }

        public IWebElement lnkLogin => Driver.FindElement(By.LinkText("Login"));

        public IWebElement lnkRegister => Driver.FindElement(By.LinkText("Register"));

        public IWebElement lnkParques => Driver.FindElement(By.LinkText("Parques"));

        public void ClickLogin() => lnkLogin.Click();

        public void ClickRegister() => lnkRegister.Click();

        public void ClickParques() => lnkParques.Click();

    }
}
