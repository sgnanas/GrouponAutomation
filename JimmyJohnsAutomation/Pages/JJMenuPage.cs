using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace JimmyJohnsAutomation.Pages
{
    public class JJMenuPage
    {
        [FindsBy(How = How.XPath, Using = "//a[@href='/menu/']")]
        [CacheLookup]
        public IWebElement MenuLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@id='ulDesktopNav']//a[@href='/find-a-jjs/']")]
        [CacheLookup]
        public IWebElement FindAJJsLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@id='ulDesktopNav']//a[@href='/about-us/']")]
        [CacheLookup]
        public IWebElement AboutUsLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@id='ulDesktopNav']//a[@href='https://www.mercury-gift.com/JimmyJohns/']")]
        [CacheLookup]
        public IWebElement GiftCardsLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@id='ulDesktopNav']//a[@href='/jj-store/']")]
        [CacheLookup]
        public IWebElement JJStoreLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@id='ulDesktopNav']//a[@href='/franchising/']")]
        [CacheLookup]
        public IWebElement FranchisingLink { get; set; }

        #region Constructor

        public JJMenuPage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(driver, this);
        }


        public IWebDriver Driver { get; set; }

        #endregion

        #region Actions


        public MenuPage GoToMenuPage()
        {
            MenuLink.Click();

            return new MenuPage(this.Driver);
        }

        #endregion
    }
}
