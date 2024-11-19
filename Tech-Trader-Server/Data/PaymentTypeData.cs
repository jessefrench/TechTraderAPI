using TechTrader.Models;

namespace TechTrader.Data
{
    public class PaymentTypeData
    {
        public static List<PaymentType> PaymentTypes = new()
        {
            new PaymentType { Id = 1, Name = "Cash" },
            new PaymentType { Id = 2, Name = "Venmo" },
            new PaymentType { Id = 3, Name = "PayPal" },
            new PaymentType { Id = 4, Name = "Apple Pay" },
            new PaymentType { Id = 5, Name = "Cash App" }
        };
    }
}