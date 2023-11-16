using OpenQA.Selenium;

namespace TflQaTask.Pages
{
    public class HomePage : CommonFragments
    {



        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public void HandleCookiePopup()
        {
            if (Wait.WaitUntilElementIdDisplayed(By.Id("cb-cookiebanner")))
                Click(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
        }
    }
}
