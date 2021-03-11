using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    class PrivacyPage
    {
        public PrivacyPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; }

        IWebElement txtPrimeiroNome => Driver.FindElement(By.Id("fname"));

        IWebElement txtSobrenome => Driver.FindElement(By.Id("lname"));

        IWebElement txtSexo => Driver.FindElement(By.Id("sexo"));

        IWebElement txtBranco => Driver.FindElement(By.XPath("/html/body/div/main/div/form/textarea"));

        IWebElement btnSubmit => Driver.FindElement(By.XPath("/html/body/div/main/div/form/input[3]"));


        //TODO
        public void Submissao()
        {

            
        }

    }
}
