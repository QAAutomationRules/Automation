using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecRun.Common.Helper;

namespace Automation.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        
        [BeforeScenario]
        public void BeforeScenario()
        {
            string browser = String.Empty;

            try
            {
                if (ConfigurationManager.AppSettings["Browser"].ToLower().IsNotNullOrEmpty())
                {
                    browser = ConfigurationManager.AppSettings["Browser"].ToLower();
                }

            }
            catch (Exception ex)
            {
                browser = String.Empty;
                Console.WriteLine(ex.Message);
            }

            //Add WebDriver to the ScenarioContext
            ScenarioContext.Current.Set(
                GetWebDriver(browser),
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
            ChromeOptions options;
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
                    driver.Manage().Window.Maximize();
                    driver.SwitchTo().DefaultContent();
                    break;

                case "":

                    // Open a Chrome browser.
                    options = new ChromeOptions();
                    options.AddArguments("--disable-extensions");
                    options.AddArguments("disable-infobars");
                    options.AddUserProfilePreference("credentials_enable_service", false);

                    //Instantiate Driver
                    driver = new ChromeDriver(Path.Combine(GetBasePath, @"Drivers\"), options);

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Manage().Window.Maximize();
                    driver.SwitchTo().DefaultContent();
                    break;

                case "ie":

                    //Instantiate Driver
                    driver = new InternetExplorerDriver(Path.Combine(GetBasePath, @"Drivers"));

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Manage().Window.Maximize();
                    driver.SwitchTo().DefaultContent();
                    break;

                case "edge":

                    //Instantiate Driver
                    driver = new EdgeDriver(Path.Combine(GetBasePath, @"Drivers"));

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Manage().Window.Maximize();
                    driver.SwitchTo().DefaultContent();
                    break;

                case "firefox":

                    Environment.SetEnvironmentVariable("webdriver.gecko.driver", Path.Combine(GetBasePath, @"Drivers\geckodriver.exe"));

                    //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Path.Combine(GetBasePath, @"Drivers\"));
                    //service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

                    ////Instantiate Driver
                    //driver = new FirefoxDriver(service);

                    FirefoxOptions ffoptions = new FirefoxOptions();
                    ffoptions.BrowserExecutableLocation = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                    driver = new FirefoxDriver(ffoptions);

                    //Set Implicit Wait time to 10 seconds
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Manage().Window.Maximize();
                    driver.SwitchTo().DefaultContent();
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
                    driver.Manage().Window.Maximize();
                    driver.SwitchTo().DefaultContent();
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
