using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace JimmyJohnsAutomation.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Open a Chrome browser.
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-extensions");
            options.AddArguments("disable-infobars");
            options.AddUserProfilePreference("credentials_enable_service", false);

            ScenarioContext.Current.Set<IWebDriver>(new ChromeDriver(Path.Combine(GetBasePath, @"Drivers\"), options),
                "IWebDriver");

        }

        [AfterScenario]
        public void AfterScenario()
        {
            //Close the Driver
            ScenarioContext.Current.Get<IWebDriver>("IWebDriver")
                .Close();

        }


        public static string GetBasePath
        {
            get
            {
                var basePath =
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                basePath = basePath.Substring(0, basePath.Length - 10);
                return basePath;
            }
        }
    }
}
