using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<List<PaymentType>> GetPaymentTypesAsync();
        Task<PaymentType> GetPaymentTypeByIdAsync(int paymentTypeId);
        Task<PaymentType> CreatePaymentTypeAsync(PaymentType PaymentType);
        Task<PaymentType> UpdatePaymentTypeAsync(int paymentTypeId, PaymentType PaymentType);
        Task<PaymentType> DeletePaymentTypeAsync(int paymentTypeId);
    }
}