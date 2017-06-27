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

        [Given(@"I go to the Jimmy John Home Page")]
        public void GivenIGoToTheJimmyJohnHomePage()
        {
            ScenarioContext.Current.Get<IWebDriver>("IWebDriver")
                .Navigate()
                .GoToUrl(ConfigurationManager.AppSettings["HomePageURL"]);
        }

    }
}
