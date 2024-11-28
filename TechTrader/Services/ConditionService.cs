using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Services
{
    public class ConditionService : IConditionService
    {
        private readonly IConditionRepository _conditionRepository;

        public ConditionService(IConditionRepository conditionRepository)
        {
            _conditionRepository = conditionRepository;
        }

        public async Task<List<Condition>> GetConditionsAsync()
        {
            return await _conditionRepository.GetConditionsAsync();
        }
    }
}