using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace JimmyJohnsAutomation.Steps
{
    [Binding]
    public sealed class NavigationSteps
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("IWebDriver");

        [Given(@"I go to the Jimmy John Home Page")]
        public void GivenIGoToTheJimmyJohnHomePage()
        {
            
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["HomePageURL"]);
        }

        [Given(@"I go to the Create account page")]
        public void GivenIGoToTheCreateAccountPage()
        {
            IWebElement loginButton = driver.FindElement(By.Id("linkOderLogin"));
            loginButton.Click();

            IWebElement createNewAccountLink = driver.FindElement(By.XPath("//a[@title='Click to Create an account']"));
            createNewAccountLink.Click();
        }

    }
}
