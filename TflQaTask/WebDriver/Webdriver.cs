using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TflQaTask.Hooks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace TflQaTask.WebDriver
{
    public class Webdriver
    {
        public IWebDriver Driver;


        public void SetDriver()
        {
            Driver = GetWebDriver(Hook.Config.Browser.ToLower());
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        private IWebDriver GetWebDriver(string browser)
        {
            switch (browser)
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    return new ChromeDriver();
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();
                default:
                    throw new Exception($"The browser {browser} is not currently implemented");
            }
        }
    }
}
