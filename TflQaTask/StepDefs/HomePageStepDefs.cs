using TflQaTask.Hooks;
using TflQaTask.Pages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace TflQaTask.StepDefs
{
    [Binding]
    public class HomePageStepDefs
    {
        private readonly HomePage _homePage;
        private readonly IWebDriver _driver;

        public HomePageStepDefs(IWebDriver driver)
        {
            _homePage = new HomePage(driver);
            _driver = driver;
        }

        [Given(@"I am on the TFL homepage")]
        public void GivenIAmOnTheTFLHomepage()
        {
            _driver.Navigate().GoToUrl(Hook.Config.Url);
            _homePage.HandleCookiePopup();
        }

    }
}
