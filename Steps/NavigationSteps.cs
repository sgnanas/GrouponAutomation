using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using JimmyJohnsAutomation.Pages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace JimmyJohnsAutomation.Steps
{
    [Binding]
    public sealed class NavigationSteps
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef
        //change for git 
        IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("IWebDriver");

        [Given(@"I go to the Jimmy John Home Page")]
        public void GivenIGoToTheJimmyJohnHomePage()
        {           
            HomePage homePage = new HomePage(driver);
            homePage
                .GoToHomePage();
        }

        [Given(@"I go to the Create account page")]
        public void GivenIGoToTheCreateAccountPage()
        {
            HomePage homePage = new HomePage(driver);
            homePage.GoToLoginPage();

            LoginPage loginPage = new LoginPage(driver);
            loginPage.GoToCreateAccountPage();
        }


        [When(@"I use the menu to navigate to the Menu Page")]
        public void WhenIUseTheMenuToNavigateToTheMenuPage()
        {
            JJMenuPage jjMenuPage = new JJMenuPage(driver);
            jjMenuPage.GoToMenuPage();
        }


        [Then(@"the Menu Page is displayed")]
        public void ThenTheMenuPageIsDisplayed()
        {
            MenuPage menuPage = new MenuPage(driver);
            menuPage.VerifyMenuPageLoaded();
        }

    }
}
