using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class PersonDAL
{
	public PersonDAL(string FilePath = "data.json" )
	{
		this.filePath = FilePath;
	}

	string filePath;

	private List<Person> dataList;

	private int lastIndex = 0;

	private void ReadData()
	{

		if (File.Exists(filePath))
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			using (StreamReader sr = new StreamReader(fs))
			{
				string json = sr.ReadToEnd();
				var people = JsonSerializer.Deserialize<PeopleWrapper>(json);
				dataList = people.People;
				lastIndex = dataList.Max(person => person.Id);
			}

		}
		else
		{
			dataList = new List<Person>();
		}
	}

	private void WriteData()
	{
		using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
		using  (StreamWriter sw = new StreamWriter(fs))
		{
			var wrapper = new PeopleWrapper { People = dataList };
			var json = JsonSerializer.Serialize(wrapper, new JsonSerializerOptions { WriteIndented = true });
			sw.Write(json);
		}

	}

	public Person? GetPerson(int id)
	{
		ReadData();
		return dataList.FirstOrDefault(person => person.Id == id);
	}
	public List<Person> GetPersonList()
	{
		ReadData();
		return dataList;
	}

	public List<Person> GetPeopleSearch(string search)
	{
		ReadData();
		return dataList.FindAll(person => person.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase));
	}

	public Person CreatePerson(Person submittedPerson)
	{
		ReadData();
		var newPerson = new Person() { Id = ++lastIndex, Name = submittedPerson.Name, Age = submittedPerson .Age};
		dataList.Add(newPerson);
		WriteData();
		return newPerson;
	}

	public Person UpdatePerson(int id, Person submittedPerson)
	{
		//todo: handle person doesn't exist

		ReadData();
		var person = dataList.FirstOrDefault(person => person.Id == id);
		if (person != null)
		{
			person.Age = submittedPerson.Age;
			person.Name = submittedPerson.Name;
		}
		WriteData();

		return person;
	}

	public void DeletePerson(int id)
	{
		ReadData();
		dataList.RemoveAll(Person => Person.Id == id);
		WriteData();
	}


}
