Feature: RenameItem
	In order to manage items types in my warehouse
	As a user
	I want to declare available items types

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