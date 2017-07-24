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
    public class LoginPage
    {
        #region WebElements

        [FindsBy(How = How.XPath, Using = "//a[@title='Click to Create an account']")]
        [CacheLookup]
        public IWebElement CreateAccountLink { get; set; }

        #endregion


        #region Constructor

        public LoginPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public IWebDriver Driver { get; set; }

        #endregion


        #region Actions

        public CreateAccountPage GoToCreateAccountPage()
        {
            CreateAccountLink.Click();

            return new CreateAccountPage(this.Driver);
        }


        #endregion

    }
}
