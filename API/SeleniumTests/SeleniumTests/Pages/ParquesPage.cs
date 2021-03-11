using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    class ParquesPage
    {
        public ParquesPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; }

        IWebElement txtCodigoPostal => Driver.FindElement(By.Name("myCountry"));

        IWebElement btnSubmeter => Driver.FindElement(By.XPath("/html/body/div/main/form/div/div/input"));


        public void Submeter(string codigoPostal)
        {

            txtCodigoPostal.SendKeys(codigoPostal);
            btnSubmeter.Submit();
        }
    }
}
