namespace TechTrader.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ListingId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public Listing Listing { get; set; }
    }
}