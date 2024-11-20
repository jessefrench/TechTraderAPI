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

            // get a single condition by id
            app.MapGet("/conditions/{conditionId}", async (IConditionService conditionService, int conditionId) =>
            {
                Condition selectedCondition = await conditionService.GetConditionByIdAsync(conditionId);
                return Results.Ok(selectedCondition);
            })
            .Produces<Condition>(StatusCodes.Status200OK);
        }
    }
}