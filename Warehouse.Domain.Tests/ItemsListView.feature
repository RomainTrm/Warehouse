Feature: ItemsListView
	As a used
	I want to administrate available items
	To manage items types in my warehouse

Scenario: Create an item
	When I create a new item "chair"
	Then I can see "chair" item in my items list

Scenario: Rename an item
	Given I created an item "chair"
	And I disabled it
	When I rename it "table"
	Then I can see "table" item in my disable items list
	
Scenario: Rename an item with an empty name
	Given I created an item "chair"
	And I disabled it
	When I rename it ""
	Then It fails
	And I can see "chair" item in my disable items list

Scenario: Rename an item without disabling it before
	Given I created an item "chair"
	When I rename it "table"
	Then It fails
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
	Then It fails
	And I can see "chair" items with 5 units in my items list
	
Scenario: Remove units to an disabled item
	Given I created an item "chair"
	And I disabled it
	When I remove 3 units
	Then It fails

Scenario: Add units to an disabled item
	Given I created an item "chair"
	And I disabled it
	When I add 3 units
	Then It fails

Scenario: Disable an item
	Given I created an item "chair"
	When I disable it
	Then I can't see "chair" item in my items list
	And I can see "chair" item in my disable items list
	
Scenario: Disable an item with units
	Given I created an item "chair"
	And I added it 5 units
	When I disable it
	Then It fails
	And I can see "chair" items with 5 units in my items list
	And I can't see "chair" item in my disable items list
	
Scenario: Enable an item
	Given I created an item "chair"
	And I disabled it
	When I enable it
	Then I can see "chair" item in my items list
	And I can't see "chair" item in my disable items list