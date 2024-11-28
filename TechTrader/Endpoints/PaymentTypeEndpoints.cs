using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class PaymentTypeEndpoints
    {
        public static void Map(WebApplication app)
        {
            // get all payment types
            app.MapGet("/payment-types", async (IPaymentTypeService paymentTypeService) =>
            {
                return await paymentTypeService.GetPaymentTypesAsync();
            })
            .Produces<List<PaymentType>>(StatusCodes.Status200OK);

            // add a payment type to a user
            app.MapPost("/payment-types/{paymentTypeId}/add/{userId}", async (IPaymentTypeService paymentTypeService, int paymentTypeId, int userId) =>
            {
                var result = await paymentTypeService.AddPaymentTypeToUserAsync(paymentTypeId, userId);
                return result;
            })
            .Produces<IResult>(StatusCodes.Status204NoContent);

            // remove a payment type from a user
            app.MapDelete("/payment-types/{paymentTypeId}/remove/{userId}", async (IPaymentTypeService paymentTypeService, int paymentTypeId, int userId) =>
            {
                var result = await paymentTypeService.RemovePaymentTypeFromUserAsync(paymentTypeId, userId);
                return result;
            })
            .Produces<IResult>(StatusCodes.Status204NoContent);
        }
    }
}