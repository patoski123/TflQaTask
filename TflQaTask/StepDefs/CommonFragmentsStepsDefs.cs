using BoDi;
using TflQaTask.Pages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace TflQaTask.StepDefs
{
    [Binding]
    public class CommonFragmentsStepsDefs
    {

        private readonly CommonFragments _commonFragments;

        public CommonFragmentsStepsDefs(IWebDriver driver)
        {
            _commonFragments = new CommonFragments(driver);
        }

        [Given(@"I navigate to '([^']*)'")]
        [When(@"I navigate to '([^']*)'")]
        public void GivenINavigateTo(string navName)
        {
            _commonFragments.SelectTopNav(navName);
        }

       
    }
}
