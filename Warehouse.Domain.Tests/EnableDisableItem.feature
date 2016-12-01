Feature: EnableDisableItem
	In order to manage items types in my warehouse
	As a user
	I want to enable or disable items

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