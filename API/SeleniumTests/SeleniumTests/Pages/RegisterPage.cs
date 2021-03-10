using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    class RegisterPage
    {
        public RegisterPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; }

        IWebElement txtUserName => Driver.FindElement(By.Name("UserName"));

        IWebElement txtEmail => Driver.FindElement(By.Name("Email"));

        IWebElement txtPassword => Driver.FindElement(By.Name("Password"));

        IWebElement btnCreate => Driver.FindElement(By.XPath("/html/body/div/main/div/div/div[4]/input[1]"));


        public void Register(string userName, string email, string password)
        {

            txtUserName.SendKeys(userName);
            txtEmail.SendKeys(email);
            txtPassword.SendKeys(password);
            btnCreate.Submit();
        }
    }
}
