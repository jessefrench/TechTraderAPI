using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Repositories
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly TechTraderDbContext dbContext;

        public PaymentTypeRepository(TechTraderDbContext context)
        {
            dbContext = context;
        }

        // get all payment types
        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            return await dbContext.PaymentTypes.ToListAsync();
        }

        // add a payment type to a user
        public async Task<IResult> AddPaymentTypeToUserAsync(int paymentTypeId, int userId)
        {
            var paymentType = await dbContext.PaymentTypes
                .Include(paymentType => paymentType.Users)
                .FirstOrDefaultAsync(paymentType => paymentType.Id == paymentTypeId);

            var user = await dbContext.Users
                .Include(user => user.PaymentTypes)
                .FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null || paymentType == null)
            {
                return Results.NotFound("User or payment type not found.");
            }

            if (user.PaymentTypes.Any(paymentType => paymentType.Id == paymentTypeId))
            {
                return Results.BadRequest("User already has this payment type.");
            }

            user.PaymentTypes.Add(paymentType);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/payment-types/{paymentTypeId}/add/{userId}", user);
        }

        // remove a payment type from a user
        public async Task<IResult> RemovePaymentTypeFromUserAsync(int paymentTypeId, int userId)
        {
            var paymentType = await dbContext.PaymentTypes
                .Include(paymentType => paymentType.Users)
                .FirstOrDefaultAsync(paymentType => paymentType.Id == paymentTypeId);

            var user = await dbContext.Users
                .Include(user => user.PaymentTypes)
                .FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null || paymentType == null)
            {
                return Results.NotFound("User or payment type not found.");
            }

            if (!user.PaymentTypes.Any(paymentType => paymentType.Id == paymentTypeId))
            {
                return Results.BadRequest("User does not have this payment type.");
            }

            user.PaymentTypes.Remove(paymentType);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }
    }
}