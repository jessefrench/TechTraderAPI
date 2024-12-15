using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly TechTraderDbContext dbContext;

        public MessageRepository(TechTraderDbContext context)
        {
            dbContext = context;
        }

        // get all user messages
        public async Task<List<Message>> GetAllMessagesAsync(int userId)
        {
            var userMessages = await dbContext.Messages
                .Where(message => (message.ReceiverId == userId) ||
                                  (message.SenderId == userId))
                .OrderByDescending(message => message.SentAt)
                .ToListAsync();

            return userMessages;
        }

        // get a single message thread
        public async Task<List<Message>> GetSingleMessageThreadAsync(int userId, int sellerId)
        {
            var messageThread = await dbContext.Messages
                .Where(message => (message.SenderId == userId && message.ReceiverId == sellerId) ||
                                  (message.SenderId == sellerId && message.ReceiverId == userId))
                .OrderBy(message => message.SentAt)
                .ToListAsync();

            return messageThread;
        }

        // get all user messages for a specific listing
        public async Task<List<Message>> GetUserMessagesByListingIdAsync(int userId, int listingId)
        {
            var listingMessages = await dbContext.Messages
                .Where(message => (message.SenderId == userId && message.ListingId == listingId) ||
                                  (message.ReceiverId == userId && message.ListingId == listingId))
                .OrderBy(message => message.SentAt)
                .ToListAsync();

            return listingMessages;
        }

        // get latest message for each conversation
        public async Task<List<Message>> GetLatestMessagesAsync(int userId)
        {
            var conversations = await dbContext.Messages
                .Include(message => message.Listing.Seller)
                .Where(message => message.ReceiverId == userId || message.SenderId == userId)
                .GroupBy(message => new
                {
                    Sender = message.SenderId < message.ReceiverId ? message.SenderId : message.ReceiverId,
                    Receiver = message.SenderId < message.ReceiverId ? message.ReceiverId : message.SenderId,
                    Listing = message.ListingId
                })
                .Select(group => group.OrderByDescending(message => message.SentAt).FirstOrDefault())
                .ToListAsync();

            return conversations;
        }

        // create a new conversation
        public async Task<Message> CreateNewConversationAsync(Message message)
        {
            if (string.IsNullOrWhiteSpace(message.Content))
                throw new ArgumentException("Message content cannot be empty.", nameof(message.Content));

            if (message.SenderId == message.ReceiverId)
                throw new ArgumentException("Sender and receiver cannot be the same user.");

            message.SentAt = DateTime.UtcNow;
            dbContext.Messages.Add(message);
            await dbContext.SaveChangesAsync();

            return message;
        }

        // update a message
        public async Task<Message> UpdateMessageAsync(int messageId, Message updatedMessage)
        {
            var messageToUpdate = await dbContext.Messages.FirstOrDefaultAsync(message => message.Id == messageId);

            if (messageToUpdate == null)
            {
                return null;
            }

            messageToUpdate.Content = updatedMessage.Content;
            await dbContext.SaveChangesAsync();

            return updatedMessage;
        }

        // delete a message
        public async Task<bool> DeleteMessageAsync(int messageId)
        {
            var message = await dbContext.Messages.FirstOrDefaultAsync(message => message.Id == messageId);

            if (message == null)
                throw new KeyNotFoundException($"Message with ID {messageId} not found.");

            dbContext.Messages.Remove(message);
            await dbContext.SaveChangesAsync();

            return true;
        }

        // delete a conversation
        public async Task<bool> DeleteConversationAsync(int userId, int sellerId)
        {
            var messages = await dbContext.Messages
                .Where(message =>
                    (message.SenderId == userId && message.ReceiverId == sellerId) ||
                    (message.SenderId == sellerId && message.ReceiverId == userId))
                .ToListAsync();

            if (!messages.Any())
                throw new KeyNotFoundException("No conversation found between the specified users.");

            dbContext.Messages.RemoveRange(messages);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}