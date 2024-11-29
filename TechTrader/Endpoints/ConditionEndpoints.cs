using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public static class ConditionEndpoints
    {
        public static void MapConditionEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/conditions").WithTags(nameof(Condition));

            // get all conditions
            group.MapGet("/", async (IConditionService conditionService) =>
            {
                return await conditionService.GetConditionsAsync();
            })
            .WithName("GetConditions")
            .WithOpenApi()
            .Produces<List<Condition>>(StatusCodes.Status200OK);
        }
    }
}