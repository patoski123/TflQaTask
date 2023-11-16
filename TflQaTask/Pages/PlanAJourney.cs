using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TflQaTask.Pages
{
    public  class PlanAJourney : BasePage
    {
        private static readonly By InputFrom = By.Id("InputFrom");
        private static readonly By InputTo = By.Id("InputTo");
        private static readonly By FirstElement = By.Id("places-extra-search-suggestion-0");
        private static readonly By JourneyStops = By.ClassName("stop-name");
        private static readonly By PlanJourneyBtn = By.Id("plan-journey-button");
        private static readonly By HeadlineText = By.ClassName("headline-container");
        private static readonly By JourneyRouteOptions = By.ClassName("expandable-box-click-through-auto-expand-not-bus-publictransport-box-show-me");
        private static readonly By LocationsResultSuggestion = By.CssSelector("div[class^='info-message']");
        private static readonly By EditJourney = By.ClassName("edit-journey");
        private static readonly By ChangeTime = By.ClassName("change-departure-time");
        private static readonly By ArrivingJourney = By.Id("arriving");
        private static readonly By JourneyDate = By.Id("Date");
        private static readonly By JourneyTime = By.Id("Time");
        private static readonly By JourneyDateDiv = By.ClassName("selector-date-of-departure");
        private static readonly By InputToError = By.XPath("//div[@id='InputTo-error']");
        private static readonly By InputToErrorId = By.Id("InputTo-error");
        private static readonly By InputFromError = By.XPath("//div[@id='InputFrom-error']");
        private static readonly By InputFromErrorId = By.Id("InputFrom-error");
        private static readonly By ValidationErrors = By.ClassName("field-validation-error");
        private static readonly By RecentPlannedJourneyList = By.CssSelector(".plain-button.journey-item");


        public PlanAJourney(IWebDriver driver) : base(driver)
        {
        }

        public List<string> GetPageErrors()
        {
            return GetWebElements(ValidationErrors).Select(x => x.Text).ToList();
        }

        public void EnterInputTo(string input)
        {
                SendKeys(input, InputTo);

        }

        public void ClickEditJourney()
        {
            Click(EditJourney);
        }

        public void EnterInputFrom(string input)
        {
                SendKeys(input, InputFrom);
        }

        public void ClickPlanJourneyBtn()
        {
            Click(PlanJourneyBtn);
        }

        public void ClickJourneyChangeTime()
        {
            Click(ChangeTime);
        }

        public void ClickJourneyArriving()
        {
            Click(ArrivingJourney);
        }

        public void ClickJourneyDepartureDate()
        {
            Click(JourneyDate);
        }


        public void SelectDepartureTime(string journeyTime)
        {
            SelectDropDownListByName(journeyTime, JourneyTime);
        }

        public void SelectDeparturedate(string journeyDate)
        {
            SelectDropDownListByName(journeyDate, JourneyDate);

        }
        public void EnterJourneyDetails(string from, string to)
        {
            if (!string.IsNullOrEmpty(from))
            {
                EnterInputFrom(from);
                SelectJourneyStops(from);
            }

            if (!string.IsNullOrEmpty(to))
            {
                EnterInputTo(to);
                SelectJourneyStops(to);
            }

            ClickPlanJourneyBtn();
        }

        public int GetRecentJourneyCount()
        {
           return GetWebElements(RecentPlannedJourneyList).Count;
        }

        public void EnterInvalidJourneyDetails(string from, string to)
        {
            EnterInputFrom(from);
            SelectJourneyStops(from);
            EnterInputTo(to);
            ClickPlanJourneyBtn();
        }

        public void SelectJourneyStops(string input)
        {
            Wait.WaitUntilElementIdDisplayed(JourneyStops);
            var element = GetWebElementContains(JourneyStops, input);
            Click(element);
        }


        public string GetHeadlineText()
        {
            return GetElementText(HeadlineText);
        }

        public bool hasResultSuggestion()
        {

            return Wait.WaitUntilElementIdDisplayed(LocationsResultSuggestion);
        }

      
    }
}
