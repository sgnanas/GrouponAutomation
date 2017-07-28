using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JimmyJohnsAutomation.WebDriverExtensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JimmyJohnsAutomation.Pages
{
    public class MenuPage
    {
        [FindsBy(How = How.XPath, Using = "//a[@class='tab menuTab pie']")]
        [CacheLookup]
        public IWebElement JJMenusTab { get; set; }


        #region Constructor

        public MenuPage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
        }


        public IWebDriver Driver { get; set; }

        #endregion


        public MenuPage VerifyMenuPageLoaded()
        {
     
            JJMenusTab.WaitForVisible(true, 1000);

            return new MenuPage(this.Driver);
        }
    }
}
