# Aeroqual C# Cloud Developer Test

## Objectives
This test is to create a simple API that serves data from a JSON file. We would like to see what you would consider production quality code.

## Instructions
- The JSON file is `data.json`, and contains a list of people, with their names and ages.
- We would like you to create a RESTful API that allows you to create, update, delete, and get people.
- In addition, we would like to be able to search by a person's name. The input may not always be a complete name, e.g. if the input is `hat`, we expect `hat`, `hatter`, `that` etc to be returned.
- You are free to layout the project however you wish. 

## Submitting Completed Test
- Please commit your code to a private github repository, and invite both `AeroqualHayden` and `maxgruebneraeroqual` as collaborators.
- If you cannot do that for some reason, please zip the source code and email it to the recruiter.

## Design decisions

Auth: 
	JWT, basic secure standard likely sufficient. Token management not implemented.
DAL:
	- Caching: not implemented, R/W done each time CRUD performed. Insufficient spec for caching/speed scope. Potential for simple caching using a 'lastwrite' check in the DAL
	- Concurrency: filestream used, rely on OS.
	- Deletion: Considering privacy obligations by default and data structure in json file, hard delete used. Requirements could specify a soft delete, or delayed/batch deletion.
Error handling:
	Very basic error handling included, scope not defined.


x /people POST 
x /people/{id} GET
x /people/{id} DELETE
x /people/{id} PUT 
x /people?{searchTerm} GET 
x /peopleGET
