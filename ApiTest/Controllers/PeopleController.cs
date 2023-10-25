using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase

{
    public PeopleController() //do dependency injection?
    {
        DAL = new PersonDAL();
    }

    public PersonDAL DAL { get; private set; }


	// GET: people?searchTerm
	[HttpGet]
	public IActionResult GetPeople([FromQuery] string searchTerm)
	{
		List<Person> peopleList;

		if (searchTerm == null)
		{
			peopleList = DAL.GetPersonList();
		}else
		{
			peopleList = DAL.GetPeopleSearch(searchTerm);

		}
		return Ok(peopleList);
	}

	// GET: people/{id}
	[HttpGet("{id}")]
	public IActionResult GetPerson(int id)
	{
		// Implement logic to get a specific person by ID
		var person = DAL.GetPerson(id);
		if (person == null)
		{
			return NotFound(); // 404 Not Found
		}
		return Ok(person);
	}

	// POST: people
	[HttpPost]
	public IActionResult CreatePerson([FromBody] Person newPerson)
	{
		var person = DAL.CreatePerson(newPerson);
		return CreatedAtAction("GetPerson", new { id = person.Id }, person);
	}

	// PUT: people/{id}
	[HttpPut("{id}")]
	public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
	{
		//todo: handle put doing a creation
		DAL.UpdatePerson(id, updatedPerson);
		return NoContent(); // 204 No Content
	}

	// DELETE: people/{id}
	[HttpDelete("{id}")]
	public IActionResult DeletePerson(int id)
	{
		DAL.DeletePerson(id);
		return NoContent();
	}
}
