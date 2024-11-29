using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public static class PaymentTypeEndpoints
    {
        public static void MapPaymentTypeEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/payment-types").WithTags(nameof(PaymentType));

            // get all payment types
            group.MapGet("/", async (IPaymentTypeService paymentTypeService) =>
            {
                return await paymentTypeService.GetPaymentTypesAsync();
            })
            .WithName("GetPaymentTypes")
            .WithOpenApi()
            .Produces<List<PaymentType>>(StatusCodes.Status200OK);

            // add a payment type to a user
            group.MapPost("/{paymentTypeId}/add/{userId}", async (IPaymentTypeService paymentTypeService, int paymentTypeId, int userId) =>
            {
                var result = await paymentTypeService.AddPaymentTypeToUserAsync(paymentTypeId, userId);
                return result;
            })
            .WithName("AddPaymentTypeToUser")
            .WithOpenApi()
            .Produces<IResult>(StatusCodes.Status204NoContent);

            // remove a payment type from a user
            group.MapDelete("/{paymentTypeId}/remove/{userId}", async (IPaymentTypeService paymentTypeService, int paymentTypeId, int userId) =>
            {
                var result = await paymentTypeService.RemovePaymentTypeFromUserAsync(paymentTypeId, userId);
                return result;
            })
            .WithName("RemovePaymentTypeFromUser")
            .WithOpenApi()
            .Produces<IResult>(StatusCodes.Status204NoContent);
        }
    }
}