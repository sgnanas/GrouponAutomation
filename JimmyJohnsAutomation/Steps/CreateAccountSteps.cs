using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Faker;
using OpenQA.Selenium.Support.UI;

namespace JimmyJohnsAutomation.Steps
{
    [Binding]
    public sealed class CreateAccountSteps
    {
       
        IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("IWebDriver");


        [When(@"I create a new account")]
        public void WhenICreateANewAccount()
        {
            IWebElement FirstNameTextBox = driver.FindElement(By.XPath("//input[@name='FirstName']"));
            IWebElement LastNameTextBox = driver.FindElement(By.XPath("//input[@name='LastName']"));
            IWebElement PhoneNumberTextBox = driver.FindElement(By.XPath("//input[@id='phone_0']"));
            IWebElement EmailTextBox = driver.FindElement(By.XPath("//input[@id='email']"));
            IWebElement ConfirmEmailTextBox = driver.FindElement(By.XPath("//input[@id='emailConfirm']"));
            IWebElement PasswordTextBox = driver.FindElement(By.XPath("//input[@id='password']"));
            IWebElement PasswordConfirmTextBox = driver.FindElement(By.XPath("//input[@id='passwordConfirm']"));
            IWebElement TermsAndConditionsCheckBox = driver.FindElement(By.XPath("//a[@id='chkTermsAndConditions']"));
            IWebElement CreateAccountButton = driver.FindElement(By.CssSelector("#createAccountBtn"));

            string email = Internet.Email();
            string password = "BlahPassword6";

            FirstNameTextBox.SendKeys(Name.First());
            LastNameTextBox.SendKeys(Name.Last());
            PhoneNumberTextBox.SendKeys(Phone.Number());
            EmailTextBox.SendKeys(email);
            ConfirmEmailTextBox.SendKeys(email);
            PasswordTextBox.SendKeys(password);
            PasswordConfirmTextBox.SendKeys(password);
            TermsAndConditionsCheckBox.Click();
            CreateAccountButton.Click();

        }

        [Then(@"the Jimmy John user is created")]
        public void ThenTheJimmyJohnUserIsCreated()
        {
            //IWebElement StartAnOrderText = 
            //    driver.FindElement(By.XPath("//h1[contains(text(),'Start an Order')]"));


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//h1[contains(text(),'Start an Order')]")));
        }


    }
}
