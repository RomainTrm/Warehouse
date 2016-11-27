Feature: ItemsListView
	As a used
	I want to administrate available items
	To manage items types in my warehouse

Scenario: Create an item
	When I create a new item "chair"
	Then I can see "chair" item in my items list

Scenario: Rename an item
	Given I created an item "chair"
	When I rename it "table"
	Then I can see "table" item in my items list
	
Scenario: Rename an item with an empty name
	Given I created an item "chair"
	When I rename it ""
	Then Rename fail
	And I can see "chair" item in my items list