using ApiTest.DataAccess;
using ApiTest.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiTest.Controllers
{

	[ApiController]
	[Route("[controller]")]
	public class PeopleController : ControllerBase

	{
		public PeopleController(IPersonDAL DAL) //do dependency injection?
		{
			_DAL = DAL;
		}

		private IPersonDAL _DAL;


		// GET: people?searchTerm
		[HttpGet]
		public IActionResult GetPeople([FromQuery] string searchTerm)
		{
			List<Person> peopleList;

			if (searchTerm == null)
			{
				peopleList = _DAL.GetPersonList();
			}
			else
			{
				peopleList = _DAL.GetPeopleSearch(searchTerm);

			}
			return Ok(peopleList);
		}

		// GET: people/{id}
		[HttpGet("{id}")]
		public IActionResult GetPerson(int id)
		{
			// Implement logic to get a specific person by ID
			var person = _DAL.GetPerson(id);
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
			var person = _DAL.CreatePerson(newPerson);
			return CreatedAtAction("GetPerson", new { id = person.Id }, person);
		}

		// PUT: people/{id}
		[HttpPut("{id}")]
		public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
		{
			var existingPerson = _DAL.UpdatePerson(id, updatedPerson);

			//returns 201 with location header if PUT creates new person, otherwise standard 204 response
			return existingPerson ? NoContent() : CreatedAtAction("GetPerson", new { id = id }, new { id = id }); 
		}

		// DELETE: people/{id}
		[HttpDelete("{id}")]
		public IActionResult DeletePerson(int id)
		{
			_DAL.DeletePerson(id);
			return NoContent();
		}
	}
}