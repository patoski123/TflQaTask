using AventStack.ExtentReports.Gherkin.Model;
using TflQaTask.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace TflQaTask.StepDefs
{
    [Binding]
    public class PlanAJourneyStepDefs
    {

        private readonly PlanAJourney _planAJourney;

        public PlanAJourneyStepDefs(PlanAJourney planAJourney)
        {
            _planAJourney = planAJourney;
        }

        [When(@"I enter location from '([^']*)' and to '([^']*)'")]
        [Then(@"I enter location from '([^']*)' and to '([^']*)'")]
        public void WhenIEnterLocationFromAndTo(string from, string to)
        {
            _planAJourney.EnterJourneyDetails(from, to);
        }

        [Then(@"I get the headline text '([^']*)' displayed")]
        public void ThenIGetTheHeadlineTextDisplayed(string headline)
        {
            Assert.That(_planAJourney.GetHeadlineText(), Is.EqualTo(headline));
        }

        [Then(@"I can select a journey route")]
        public void ThenICanSelectAJourneyRoute()
        {
            
        }

        [When(@"I plan a journey with invalid locations from '([^']*)' and to '([^']*)'")]
        public void WhenIPlanAJourneyWithInvalidLocationsFromAndTo(string from, string to)
        {
            _planAJourney.EnterInvalidJourneyDetails(from, to);

        }

        [Then(@"The journey route results should not be displayed")]
        public void ThenTheJourneyRouteResultsShouldNotBeDisplayed()
        {
            Assert.That(_planAJourney.GetHeadlineText(), Is.EqualTo("Journey results"));
            Assert.IsTrue(_planAJourney.hasResultSuggestion());
        }

        [Then(@"I should see matching locations suggestions")]
        public void ThenIShouldSeeMatchingLocationsSuggestions()
        {
            
        }

        [Given(@"I plan the journey based on arrival time '([^']*)' '([^']*)'")]
        public void WhenIPlanTheJourneyBasedOnArrivalTime(string tommorow, string arrivalTime)
        {
            _planAJourney.ClickJourneyChangeTime();
            _planAJourney.ClickJourneyArriving();
            _planAJourney.ClickJourneyDepartureDate();
            _planAJourney.SelectDeparturedate(tommorow);
            _planAJourney.SelectDepartureTime(arrivalTime);
        }



        [Then(@"The journey route results should be displayed")]
        public void ThenTheJourneyRouteResultsShouldBeDisplayed()
        {
            Assert.That(_planAJourney.GetHeadlineText(), Is.EqualTo("Journey results"));
            Assert.IsFalse(_planAJourney.hasResultSuggestion());
        }

        [Then(@"I can edit the journey to location value '([^']*)'")]
        public void ThenICanEditTheJourneyToLocationValue(string inputTo)
        {
            _planAJourney.ClickEditJourney();
            _planAJourney.EnterInputTo(inputTo);
            _planAJourney.SelectJourneyStops(inputTo);
            _planAJourney.ClickPlanJourneyBtn();
        }


        [When(@"I enter locations from value (.*), to location value (.*) into the widget")]
        public void WhenIEnterLocations(string fromLocation, string toLocation)
        {
            _planAJourney.EnterJourneyDetails(fromLocation, toLocation);
            
           
        }

        [Then(@"I should get a validation error for the empty location (.*) field")]
        public void ThenIShouldGetAValidationErrorForTheEmptyLocation(string field)
        {
            List<string> errorsNotFound = new List<string>();
            var errorsToFind = field.Split(',');
            var pageErrors = _planAJourney.GetPageErrors();
            foreach ( var error in errorsToFind ) {
                if(!pageErrors.Contains(error))
                    errorsNotFound.Add(error);
            }
            
            Assert.IsTrue(errorsNotFound.Count == 0, $"The following errors were not found on page {string.Join(" ,", errorsNotFound)}");
        }

        [Then(@"The recent tab should contain the (.*) planned journeys")]
        public void ThenTheRecentTabShouldContainThePlannedJourneys(int recentJourneyCount)
        {
            Assert.That(_planAJourney.GetRecentJourneyCount(), Is.EqualTo(recentJourneyCount));
        }

    }
}
