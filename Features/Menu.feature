Feature: Menu
	As a Jimmy John customer
	I want to be able to Navigate to different pages via a Menu

@SMOKE
Scenario: Navigate to the Menu page
	Given I go to the Jimmy John Home Page
	When I use the menu to navigate to the Menu Page
	Then the Menu Page is displayed
