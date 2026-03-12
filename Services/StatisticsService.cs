using System;
using System.Linq;
using AvaloniaDIContainer.Models;

namespace AvaloniaDIContainer.DB;

public class StatisticsService: IStatisticsService
{
    private readonly IPersonRepository _personRepository;

    public StatisticsService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    public int GetTotalCount()
    {
        return _personRepository.GetAll().Count();
    }

    public double GetAverageAge()
    {
        var persons = _personRepository.GetAll();

        if (!persons.Any())
            return 0;
        
        return persons.Average(p => p.Age);
    }
    
    public Person? GetOldestPerson()
    {
        var persons = _personRepository.GetAll();
        
        if (!persons.Any())
            return null;
        return persons.OrderBy(p => p.Age).LastOrDefault();
    }

    public int GetCountByAgeRange(int minAge, int maxAge)
    {
        if (minAge < 0)
            throw new ArgumentException("Не может быть возраст < 0", nameof(minAge));
        
        if (maxAge < minAge)
            throw new ArgumentException("Максимальный возраст не может быть < минимального", nameof(maxAge));
    
        var persons = _personRepository.GetAll();
        return persons.Count(p => p.Age >= minAge && p.Age <= maxAge);
    }
}