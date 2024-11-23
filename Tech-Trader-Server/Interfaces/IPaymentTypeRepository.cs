using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<List<PaymentType>> GetPaymentTypesAsync();
        Task<IResult> AddPaymentTypeToUserAsync(int paymentTypeId, int userId);
        Task<IResult> RemovePaymentTypeFromUserAsync(int paymentTypeId, int userId);
    }
}