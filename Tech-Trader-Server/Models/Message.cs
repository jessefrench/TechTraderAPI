namespace TechTrader.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SellerId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}