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

            // get a single payment type by id
            app.MapGet("/payment-types/{paymentTypeId}", async (IPaymentTypeService paymentTypeService, int paymentTypeId) =>
            {
                PaymentType selectedPaymentType = await paymentTypeService.GetPaymentTypeByIdAsync(paymentTypeId);
                return Results.Ok(selectedPaymentType);
            })
            .Produces<PaymentType>(StatusCodes.Status200OK);

            // create a new payment type
            app.MapPost("/payment-types", async (IPaymentTypeService paymentTypeService, PaymentType paymentType) =>
            {
                var newPaymentType = await paymentTypeService.CreatePaymentTypeAsync(paymentType);
                return Results.Created($"/payment-types/{paymentType.Id}", paymentType);
            })
            .Produces<PaymentType>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a payment-type
            app.MapPut("/payment-types/{paymentTypeId}", async (IPaymentTypeService paymentTypeService, int paymentTypeId, PaymentType updatedPaymentType) =>
            {
                var paymentTypeToUpdate = await paymentTypeService.UpdatePaymentTypeAsync(paymentTypeId, updatedPaymentType);
                return Results.Ok(paymentTypeToUpdate);
            })
            .Produces<PaymentType>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

            // delete a payment type
            app.MapDelete("/payment-types/{paymentTypeId}", async (IPaymentTypeService paymentTypeService, int paymentTypeId) =>
            {
                var paymentTypeToDelete = await paymentTypeService.DeletePaymentTypeAsync(paymentTypeId);
                return Results.NoContent();
            })
            .Produces<PaymentType>(StatusCodes.Status204NoContent);
        }
    }
}