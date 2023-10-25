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

### Auth: 
	Auth not implemented - not specified. Would default to using JWT if required, assume in most prod cases auth is a separate build
### DAL:
	- DTO mapping uses default .net JSON mapping
	- Caching: not implemented, R/W done each time CRUD performed. Insufficient spec for caching/speed scope. Potential for simple caching using a 'last write' check in the DAL
	- Concurrency: filestream used, rely on OS.
	- Deletion: Considering privacy obligations by default and data structure in json file, hard delete used. Requirements could specify a soft delete, or delayed/batch deletion.
### Responses/Errors:
	404 thrown for GET by ID failing, as per RFC
	empty list returned for no result search - no error to handle
	PUT will create a resource if none exists, and inform client with 201 response
	File access issues will cause catastrophic failure, throw 500 response. 
### Layout: 
	Single controller used, automatically registered by convention 
	Separate Data Access Layer used by controller 
	Basic namespaces/folder structure
	Interface in same file as implementation: Easier dev experience when implementation is in the same project in my experience.

### Expanded scope could include
	Performance improvements i.e: caching
	Closer look at concurrency, async, and file flags if multiple services running off one file
	Using a database.
	Implementing authorisation.
	
	

Endpoints:

- /people POST : create person (JSON body object)
- /people/{id} GET : get person by id
- /people/{id} DELETE : delete person by id
- /people/{id} PUT : update person by id (JSON body object)
- /people?{searchTerm} GET : get list of people by substring match (case-insensitive)
- /people GET : get complete list of people

