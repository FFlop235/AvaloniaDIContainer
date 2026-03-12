using System.Collections.Generic;
using AvaloniaDIContainer.Models;

namespace AvaloniaDIContainer.DB;

public interface IPersonRepository
{
    IEnumerable<Person> GetAll();
    void Add(Person person);
    void Update(Person person);
    void Delete(Person person);
    Person? GetById(string id);
}