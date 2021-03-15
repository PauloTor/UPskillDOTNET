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

        IWebElement txtFirstName => Driver.FindElement(By.Id("FirstName"));

        IWebElement txtLastName => Driver.FindElement(By.Id("LastName"));

        IWebElement txtUsername => Driver.FindElement(By.Id("Username"));

        IWebElement txtEmail => Driver.FindElement(By.Id("Email"));

        IWebElement txtPassword => Driver.FindElement(By.Id("Password"));

        IWebElement txtNif => Driver.FindElement(By.Id("Nif"));

        IWebElement txtMetodoPagamento => Driver.FindElement(By.Id("MetodoPagamento"));


        IWebElement btnRegistar => Driver.FindElement(By.XPath("/html/body/div/main/div/div/div/form/div[8]/input[1]"));

        IWebElement btnLimpar => Driver.FindElement(By.XPath("/html/body/div/main/div/div/div/form/div[8]/input[2]"));

        


        public void Register(string firstName, string lastName, string userName, string email, string password, string nif, string metodoPagamento)
        {
            txtFirstName.SendKeys(firstName);
            txtLastName.SendKeys(lastName);
            txtUsername.SendKeys(userName);
            txtEmail.SendKeys(email);
            txtPassword.SendKeys(password);
            txtNif.SendKeys(nif);
            txtMetodoPagamento.SendKeys(metodoPagamento);
            btnRegistar.SendKeys(Keys.Enter);
        }
    }
}
