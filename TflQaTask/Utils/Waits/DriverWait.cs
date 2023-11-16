using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TflQaTask.Utils.Waits
{
    public class DriverWait
    {
        private readonly WebDriverWait _wait;
        public DriverWait(IWebDriver driver)
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public bool WaitUntilElementIdDisplayed(By locator)
        {

            try
            {
                return _wait.Until(d => d.FindElement(locator).Displayed);
            }
            catch
            {
                return false;
            }

        }

            

    }
}