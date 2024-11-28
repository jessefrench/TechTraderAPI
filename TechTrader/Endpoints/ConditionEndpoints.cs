using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class ConditionEndpoints
    {
        public static void Map(WebApplication app)
        {
            // get all conditions
            app.MapGet("/conditions", async (IConditionService conditionService) =>
            {
                return await conditionService.GetConditionsAsync();
            })
            .Produces<List<Condition>>(StatusCodes.Status200OK);
        }
    }
}