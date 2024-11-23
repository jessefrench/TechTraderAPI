namespace TechTrader.Models
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}