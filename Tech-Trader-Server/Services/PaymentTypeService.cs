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

        public async Task<PaymentType> GetPaymentTypeByIdAsync(int paymentTypeId)
        {
            return await _paymentTypeRepository.GetPaymentTypeByIdAsync(paymentTypeId);
        }

        public async Task<PaymentType> CreatePaymentTypeAsync(PaymentType paymentType)
        {
            return await _paymentTypeRepository.CreatePaymentTypeAsync(paymentType);
        }

        public async Task<PaymentType> UpdatePaymentTypeAsync(int paymentTypeId, PaymentType paymentType)
        {
            return await _paymentTypeRepository.UpdatePaymentTypeAsync(paymentTypeId, paymentType);
        }

        public async Task<PaymentType> DeletePaymentTypeAsync(int paymentTypeId)
        {
            return await _paymentTypeRepository.DeletePaymentTypeAsync(paymentTypeId);
        }
    }
}