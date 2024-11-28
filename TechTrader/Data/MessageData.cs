using TechTrader.Models;

namespace TechTrader.Data
{
    public class MessageData
    {
        public static List<Message> Messages = new()
        {
            new Message
            {
                Id = 1,
                SenderId = 2,
                ReceiverId = 1,
                ListingId = 1,
                Content = "Hi, is this item still available?",
                SentAt = new DateTime(2024, 11, 18, 10, 30, 0)
            },
            new Message
            {
                Id = 2,
                SenderId = 1,
                ReceiverId = 2,
                ListingId = 1,
                Content = "Yes, it's available. Would you like to arrange a pickup?",
                SentAt = new DateTime(2024, 11, 18, 10, 35, 0)
            },
            new Message
            {
                Id = 3,
                SenderId = 3,
                ReceiverId = 1,
                ListingId = 2,
                Content = "Can you provide more details about the condition?",
                SentAt = new DateTime(2024, 11, 18, 11, 15, 0)
            },
            new Message
            {
                Id = 4,
                SenderId = 2,
                ReceiverId = 3,
                ListingId = 3,
                Content = "When are you available to meet?",
                SentAt = new DateTime(2024, 11, 18, 12, 45, 0)
            },
            new Message
            {
                Id = 5,
                SenderId = 1,
                ReceiverId = 3,
                ListingId = 4,
                Content = "I'm interested. Is the price negotiable?",
                SentAt = new DateTime(2024, 11, 18, 13, 10, 0)
            }
        };
    }
}