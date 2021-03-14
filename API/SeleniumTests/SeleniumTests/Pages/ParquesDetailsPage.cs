using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    class ParquesDetailsPage
    {
        public ParquesDetailsPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebDriver Driver { get; }

        IWebElement datainicio => Driver.FindElement(By.Id("datainicio"));

        IWebElement datafim => Driver.FindElement(By.Id("datafim"));

        IWebElement btnCriarReserva => Driver.FindElement(By.XPath("/html/body/div/main/form/div/input"));


        public void SubmeterDataInicio(string dataInicio)
        {
            datainicio.SendKeys(dataInicio);
        }
        public void SubmeterDataFim(string dataFim)
        {

            datafim.SendKeys(dataFim);

            btnCriarReserva.SendKeys(Keys.Space);
        }
    }
}
