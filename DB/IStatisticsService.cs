using AvaloniaDIContainer.Models;

namespace AvaloniaDIContainer.DB;

public interface IStatisticsService
{
    int GetTotalCount();
    double GetAverageAge();
    Person? GetOldestPerson();
    int GetCountByAgeRange(int minAge, int maxAge);
}