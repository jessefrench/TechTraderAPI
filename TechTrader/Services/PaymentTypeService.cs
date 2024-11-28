using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            return await _paymentTypeRepository.GetPaymentTypesAsync();
        }

        public async Task<IResult> AddPaymentTypeToUserAsync(int paymentTypeId, int userId)
        {
            return await _paymentTypeRepository.AddPaymentTypeToUserAsync(paymentTypeId, userId);
        }

        public async Task<IResult> RemovePaymentTypeFromUserAsync(int paymentTypeId, int userId)
        {
            return await _paymentTypeRepository.RemovePaymentTypeFromUserAsync(paymentTypeId, userId);
        }
    }
}