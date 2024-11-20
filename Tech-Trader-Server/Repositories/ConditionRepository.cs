using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Repositories
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly TechTraderDbContext dbContext;

        public ConditionRepository(TechTraderDbContext context)
        {
            dbContext = context;
        }

        // get all conditions
        public async Task<List<Condition>> GetConditionsAsync()
        {
            return await dbContext.Conditions.ToListAsync();
        }

        // get a single condition by id
        public async Task<Condition> GetConditionByIdAsync(int conditionId)
        {
            Condition selectedCondition = await dbContext.Conditions.FirstOrDefaultAsync(condition => condition.Id == conditionId);
            return selectedCondition;
        }
    }
}