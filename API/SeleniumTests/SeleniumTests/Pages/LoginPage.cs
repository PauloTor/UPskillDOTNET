using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    class LoginPage
    {

        public LoginPage (IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; }

        IWebElement txtUserName => Driver.FindElement(By.Name("Email"));

        IWebElement txtPassword => Driver.FindElement(By.Name("Password"));

        IWebElement btnLogin => Driver.FindElement(By.XPath("/html/body/div/main/div/div/div/form/div[3]/input"));


        public void Login(string email, string password)
        {

            txtUserName.SendKeys(email);
            txtPassword.SendKeys(password);
            btnLogin.Submit();
        }

    }
}
