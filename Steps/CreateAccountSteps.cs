using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Faker;
using OpenQA.Selenium.Support.UI;
using Faker.Extensions;
using JimmyJohnsAutomation.WebDriverExtensions;
using JimmyJohnsAutomation.Pages;

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


            string phone = Phone.Number();
            if (phone.Substring(0, 2).Equals("1-"))
            {
                phone = phone.Substring(2, phone.Length - 2);
            }
            if (phone.Contains("x"))
            {
                int index = phone.IndexOf("x");
                phone = phone.Remove(index - 1);
            }
            phone = phone.Replace(".", "");
            phone = phone.Replace("-", "");
            phone = phone.Replace("(", "");
            phone = phone.Replace(")", "");
            phone = phone.Replace(" ", "");
        
            
            FirstNameTextBox.SetText(Name.First());
            LastNameTextBox.SetText(Name.Last());
            PhoneNumberTextBox.SetText(phone);
            EmailTextBox.SetText(email);
            ConfirmEmailTextBox.SetText(email);
            PasswordTextBox.SetText(password);
            PasswordConfirmTextBox.SetText(password);
            TermsAndConditionsCheckBox.SetCheckbox(true);
            CreateAccountButton.Click();

        }

        [When(@"I create a new account with a Page Object")]
        public void WhenICreateANewAccountWithAPageObject()
        {
            CreateAccountPage createAccountPage = new CreateAccountPage(driver);
            createAccountPage.CreateAccount();
            
        }

        
        [Then(@"the Jimmy John user is created")]
        public void ThenTheJimmyJohnUserIsCreated()
        {
            //IWebElement StartAnOrderText = 
            //    driver.FindElement(By.XPath("//h1[contains(text(),'Start an Order')]"));


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//h1[contains(text(),'Start an Order')]")));
        }


    }
}
