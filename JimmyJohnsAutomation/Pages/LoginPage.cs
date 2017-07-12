using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JimmyJohnsAutomation.Pages
{
    class LoginPage
    {
        [FindsBy(How = How.XPath, Using = "//a[@title='Click to Create an account']")]
        [CacheLookup]
        public IWebElement CreateAccountLink { get; set; }

        public LoginPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }


        public IWebDriver Driver { get; set; }

        public void GoToCreateAccountPage()
        {
            CreateAccountLink.Click();
        }
    }
}
