Feature: AddRemoveUnits
	In order to kown what is in my warehouse
	As a user
	I want to add and remove units
	
Scenario: Add units to an item
	Given I created an item "chair"
	And I added 5 units
	When I add 3 units
	Then I can see "chair" items with 8 units in my items list
	And It contains "Add 3 unit(s)." in the history
	
Scenario: Add units to an disabled item
	Given I created an item "chair"
	And I disabled it
	When I add 3 units
	Then It fails

Scenario: Remove units to an item
	Given I created an item "chair"
	And I added 5 units
	When I remove 3 units
	Then I can see "chair" items with 2 units in my items list
	And It contains "Remove 3 unit(s)." in the history

Scenario: Remove too much units to an item
	Given I created an item "chair"
	And I added 5 units
	When I remove 7 units
	Then It fails
	And I can see "chair" items with 5 units in my items list
	
Scenario: Remove units to an disabled item
	Given I created an item "chair"
	And I disabled it
	When I remove 3 units
	Then It fails