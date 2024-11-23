using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface IConditionService
    {
        Task<List<Condition>> GetConditionsAsync();
    }
}