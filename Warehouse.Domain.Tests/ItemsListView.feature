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
	Then It fail
	And I can see "chair" item in my items list

Scenario: Add units to an item
	Given I created an item "chair"
	And I added it 5 units
	When I add 3 units
	Then I can see "chair" items with 8 units in my items list

Scenario: Remove units to an item
	Given I created an item "chair"
	And I added it 5 units
	When I remove 3 units
	Then I can see "chair" items with 2 units in my items list

Scenario: Remove too much units to an item
	Given I created an item "chair"
	And I added it 5 units
	When I remove 7 units
	Then It fail
	And I can see "chair" items with 5 units in my items list

Scenario: Disable an item
	Given I created an item "chair"
	When I disable it
	Then I can't see "chair" item in my items list
	And I can see "chair" item in my disable items list