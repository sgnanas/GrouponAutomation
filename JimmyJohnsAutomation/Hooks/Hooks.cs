using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
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

            ScenarioContext.Current.Set(
                GetWebDriver(ConfigurationManager.AppSettings["Browser"].ToLower()),
                "IWebDriver");            

        }

        [AfterScenario]
        public void AfterScenario()
        {
            //Close the Driver
            ScenarioContext.Current.Get<IWebDriver>("IWebDriver")
                .Close();

        }


        public static IWebDriver GetWebDriver(string browser)
        {
            ChromeOptions options = new ChromeOptions();
            IWebDriver driver;

            switch (browser)
            {
                case "chrome":

                    // Open a Chrome browser.
                    options = new ChromeOptions();
                    options.AddArguments("--disable-extensions");
                    options.AddArguments("disable-infobars");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    
                    //Instantiate Driver
                    driver = new ChromeDriver(Path.Combine(GetBasePath, @"Drivers\"), options);

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    break;

                case "ie":
                    
                    //Instantiate Driver
                    driver = new InternetExplorerDriver(Path.Combine(GetBasePath, @"Drivers"));

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    break;

                case "firefox":

                    //Environment.SetEnvironmentVariable("webdriver.gecko.driver", Path.Combine(GetBasePath, @"Drivers\geckodriver.exe"));
                    
                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Path.Combine(GetBasePath, @"Drivers\"));
                    service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

                    //Instantiate Driver
                    driver = new FirefoxDriver(service);

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    break;

                case "phantomjs":

                    //Instantiate Driver
                    driver = new PhantomJSDriver(Path.Combine(GetBasePath, @"Drivers\"));

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    break;

                default:

                    // Open a Chrome browser.
                    options = new ChromeOptions();
                    options.AddArguments("--disable-extensions");
                    options.AddArguments("disable-infobars");
                    options.AddUserProfilePreference("credentials_enable_service", false);

                    //Instantiate Driver
                    driver = new ChromeDriver(Path.Combine(GetBasePath, @"Drivers\"), options);

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    break;

            }

            return driver;
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
