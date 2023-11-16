using TflQaTask.Utils.Waits;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TflQaTask.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;
        protected DriverWait Wait;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new DriverWait(driver);
        }

        public string GetElementText(By locator)
            => Driver.FindElement(locator).Text;


        public void Click(By locator)
        { 
            Click(Driver.FindElement(locator));
        }
          

        public void Click(IWebElement webElement, bool waitForAjax = true)
        {
            webElement.Click();
        }

        public void SendKeys(string text, By locator, bool clearText = true)
        {
            var element = Driver.FindElement(locator);
            MoveToElement(element);
            if (clearText)
                element.Clear();
            element.SendKeys(text);
        }


        public void SelectDropDownListByName(string name, By locator)
        {
            if (string.IsNullOrEmpty(name))
                return;

            var element = new SelectElement(Driver.FindElement(locator));
            element.SelectByText(name);
        }

        public void MoveToElement(IWebElement element)
        {
            new Actions(Driver).MoveToElement(element).Perform(); 
        }



        public IWebElement GetWebElement(By locator, string text)
        {
            return GetWebElements(locator).FirstOrDefault(
                       ele => ele.Text.Trim().Equals(text))
                   ?? throw new Exception($"Unable to find element where text is : {text}");
        }

        public IWebElement GetWebElementContains(By locator, string text)
        {
            return GetWebElements(locator).FirstOrDefault(
                       ele => ele.Text.Trim().ToLower().Contains(text.ToLower()))
                   ?? throw new Exception($"Unable to find element where text contains : {text}");
        }




        public IList<IWebElement> GetWebElements(By locator)
        {
            return Driver.FindElements(locator);
        }




    }
}