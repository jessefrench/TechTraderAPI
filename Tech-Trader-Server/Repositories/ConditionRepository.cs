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
    }
}