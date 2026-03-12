using System.Collections.Generic;
using System.Linq;
using AvaloniaDIContainer.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaDIContainer.DB;

public class PersonRepository: IPersonRepository
{
    private static List<Person> Persons = new List<Person>();
    
    public PersonRepository()
    {
        Add(new Person
        {
            Name = "John Doe",
            Email = "1@email.com",
            Age = 20
        });
        Add(new Person
        {
            Name = "Steve Jackson",
            Email = "2@email.com",
            Age = 21
        });
        Add(new Person
        {
            Name = "Sladick Volop",
            Email = "3@email.com",
            Age = 22
        });
    }
    
    public void Add(Person person)
    {
        Persons.Add(person);
    }
    
    public void Update(Person person)
    {
        var existingPerson = GetById(person.Id);
        if (existingPerson != null)
        {
            existingPerson.Name = person.Name;
            existingPerson.Email = person.Email;
            existingPerson.Age = person.Age;
        }
        else
        {
            throw new KeyNotFoundException($"Человек с id: {person.Id} не найден!");
        }
    }
    
    public void Delete(Person person)
    {
        Persons.Remove(person);
    }
    
    public IEnumerable<Person> GetAll()
    {
        return Persons;
    }

    public Person? GetById(string id)
    {
        return Persons.FirstOrDefault(person => person.Id == id);
    }
}