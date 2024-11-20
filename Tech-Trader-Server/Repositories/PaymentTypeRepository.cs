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

        // get a single payment type by id
        public async Task<PaymentType> GetPaymentTypeByIdAsync(int paymentTypeId)
        {
            PaymentType selectedPaymentType = await dbContext.PaymentTypes.FirstOrDefaultAsync(paymentType => paymentType.Id == paymentTypeId);
            return selectedPaymentType;
        }

        // create a payment type
        public async Task<PaymentType> CreatePaymentTypeAsync(PaymentType paymentType)
        {
            await dbContext.PaymentTypes.AddAsync(paymentType);
            await dbContext.SaveChangesAsync();
            return paymentType;
        }

        // update a payment type
        public async Task<PaymentType> UpdatePaymentTypeAsync(int paymentTypeId, PaymentType updatedPaymentType)
        {
            var paymentTypeToUpdate = await dbContext.PaymentTypes.FirstOrDefaultAsync(paymentType => paymentType.Id == paymentTypeId);

            if (paymentTypeToUpdate == null)
            {
                return null;
            }

            paymentTypeToUpdate.Name = updatedPaymentType.Name;

            await dbContext.SaveChangesAsync();
            return updatedPaymentType;
        }

        // delete a payment type
        public async Task<PaymentType> DeletePaymentTypeAsync(int paymentTypeId)
        {
            var paymentTypeToDelete = await dbContext.PaymentTypes.FirstOrDefaultAsync(paymentType => paymentType.Id == paymentTypeId);

            if (paymentTypeToDelete == null)
            {
                return null;
            }

            dbContext.PaymentTypes.Remove(paymentTypeToDelete);
            await dbContext.SaveChangesAsync();
            return paymentTypeToDelete;
        }
    }
}