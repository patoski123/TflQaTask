
using OpenQA.Selenium;

namespace TflQaTask.Pages
{
    public class CommonFragments : BasePage
    {
        private static readonly By TopNavs = By.CssSelector("ul[class='collapsible-menu clearfix'] > li");
      
        public CommonFragments(IWebDriver driver)
            : base(driver)
        {

        }

        public void SelectTopNav(string navName)
        {
            var element = GetWebElement(TopNavs, navName);
            Click(element);

        }

     
    }
}