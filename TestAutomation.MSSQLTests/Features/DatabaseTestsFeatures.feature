Feature: DatabaseTestsFeatures
	In order to access database
	As a valid database user
	I want to be query database tables

#ExecuteScalar
@Unit
Scenario: ExecuteScalar - Get Avengers information from database
	Given I query for Avengers Infromation
	Then I get required details from database table

#ExecuteScalar
@Unit
Scenario: ExecuteScalar - Get Avengers information from database dynamically
	Given I query for avenger 'Captain America' Infromation
	Then I get required details from database table

#ExecuteNonQuery
@Unit
Scenario: ExecuteNonQuery - Insert reocrds for Avengers movies
	Given I Insert reocrds for Avengers movies
	Then records should be available in Avenger's Moviebase

#DataReader
@Unit
Scenario: DataReader - Get Avengers Characteristics
	Given I query for Avengers Characteristics
	Then I get characteristics for given character name

#DataTables using DataAdapter
@Unit
Scenario: DataTables - Get Avengers Visible Characteristics
	Given I query for Avengers Visible Characteristics
	Then I get visible characteristics for given character name

#Store Procedure
@Unit
Scenario Outline: Store Procedure - Insert reocrds for Avengers movies using Store Procedure
	Given I Insert reocrds for Avengers movies <Id>, <MovieName> and <YearOfRelease> using Store Procedure
	Then records should be inserted in Avenger's Moviebase for <Id>, <MovieName> and <YearOfRelease>
Examples: 
| Id | MovieName       | YearOfRelease |
| 10 | Scarlet's Witch | 2014          |