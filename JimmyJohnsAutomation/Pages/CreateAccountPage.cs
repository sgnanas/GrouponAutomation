using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JimmyJohnsAutomation.Pages
{
    public class CreateAccountPage
    {

        #region WebElements
        
        [FindsBy(How = How.XPath, Using = "//input[@name='FirstName']")]
        [CacheLookup]
        public IWebElement FirstNameTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='LastName']")]
        [CacheLookup]
        public IWebElement LastNameTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='phone_0']")]
        [CacheLookup]
        public IWebElement PhoneNumberTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='email']")]
        [CacheLookup]
        public IWebElement EmailTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='emailConfirm']")]
        [CacheLookup]
        public IWebElement ConfirmEmailTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='password']")]
        [CacheLookup]
        public IWebElement PasswordTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='passwordConfirm']")]
        [CacheLookup]
        public IWebElement PasswordConfirmTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='chkTermsAndConditions']")]
        [CacheLookup]
        public IWebElement TermsAndConditionsCheckBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='createAccountBtn']")]
        [CacheLookup]
        public IWebElement CreateAccountButton { get; set; }


        #endregion


        #region Constructor

        public CreateAccountPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }


        public IWebDriver Driver { get; set; }


        #endregion


        #region Actions

        public void CreateAccount()
        {
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

            FirstNameTextBox.SendKeys(Name.First());
            LastNameTextBox.SendKeys(Name.Last());
            PhoneNumberTextBox.SendKeys(phone);
            EmailTextBox.SendKeys(email);
            ConfirmEmailTextBox.SendKeys(email);
            PasswordTextBox.SendKeys(password);
            PasswordConfirmTextBox.SendKeys(password);
            TermsAndConditionsCheckBox.Click();
            CreateAccountButton.Click();
        }

        #endregion 

    }
}
