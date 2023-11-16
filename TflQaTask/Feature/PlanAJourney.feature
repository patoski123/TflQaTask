Feature: PlanAJourney

A short summary of the feature
Background:
	Given I am on the TFL homepage
	And I navigate to 'Plan a journey'

@webLogin
Scenario: Verify that a valid journey can be planned using the widget
	When I enter location from 'N9 0GX' and to 'Tottenham hale station'
	Then The journey route results should be displayed
	And I can select a journey route
  
Scenario: Verify that the widget is unable to provide results when an invalid journey is planned
	When I plan a journey with invalid locations from 'N9 0GX' and to 'Ajegunle Agidingbi'
	Then The journey route results should not be displayed
  

Scenario Outline: Verify that the widget is unable to plan a journey if no locations are entered into the widget
	When I enter locations from value <fromLocation>, to location value <toLocation> into the widget
	Then I should get a validation error for the empty location <errors> field

Examples:
	| fromLocation | toLocation | errors                                                |
	| N9 0GX       |            | The To field is required.                             |
	|              | N9 0GX     | The From field is required.                           |
	|              |            | The From field is required.,The To field is required. |


Scenario: Verify change time link on the journey planner displays “Arriving” option and plan a journey based on arrival time
	And I plan the journey based on arrival time 'Tomorrow' '09:00'
	When I enter location from 'N9 0GX' and to 'Tottenham hale station'
	Then The journey route results should be displayed
	And I can select a journey route

Scenario: On the Journey results page, verify that a journey can be amended by using the “Edit Journey” button
	When I enter location from 'N9 0GX' and to 'Tottenham hale station'
	Then The journey route results should be displayed
	And I can edit the journey to location value 'Liverpool Street Station'
	Then The journey route results should be displayed
	And I can select a journey route

  Scenario: Verify that the “Recents” tab on the widget displays a list of recently planned journeys
    When I enter location from 'N9 0GX' and to 'Tottenham hale station'
	Then The journey route results should be displayed
	And I can edit the journey to location value 'Liverpool Street Station'
	And The journey route results should be displayed
	When I navigate to 'Plan a journey'
	Then The recent tab should contain the 2 planned journeys
  
  
  