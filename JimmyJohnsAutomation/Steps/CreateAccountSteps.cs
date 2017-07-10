using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

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
            IWebElement CreateAccountButton = driver.FindElement(By.XPath("//a[@id='createAccountBtn']"));


            FirstNameTextBox.SendKeys("Jimmy7");
            LastNameTextBox.SendKeys("JOBO7");
            PhoneNumberTextBox.SendKeys("3039139999");
            EmailTextBox.SendKeys("ktice@blah7.com");
            ConfirmEmailTextBox.SendKeys("ktice@blah7.com");
            PasswordTextBox.SendKeys("BlahPassword6");
            PasswordConfirmTextBox.SendKeys("BlahPassword6");
            TermsAndConditionsCheckBox.Click();
            CreateAccountButton.Click();

        }

        [Then(@"the Jimmy John user is created")]
        public void ThenTheJimmyJohnUserIsCreated()
        {
            IWebElement StartAnOrderText = 
                driver.FindElement(By.XPath("//h1[contains(text(),'Start an Order')]"));
        }


    }
}
