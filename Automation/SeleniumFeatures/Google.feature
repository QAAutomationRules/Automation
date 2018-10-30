Feature: Google Search
	In order to search for website results
	As a Google search user
	I want to be able to type in a search word and receive relevant Website results

Background: Go to Google Home Page
	Given I go to the Google Home Page

@SMOKE
Scenario Outline: Search for relevant Websites from the Google Search Page given a Search word
	Given there is a search word <Word> into the Google search field
	When I press the Search button
	Then the search results should display Websites that are related to the search word

Examples:

| Word       |
| Cheese     | 
| Hamburgers |

