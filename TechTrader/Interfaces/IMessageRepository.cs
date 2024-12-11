using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllMessagesAsync(int userId);
        Task<List<Message>> GetSingleMessageThreadAsync(int userId, int sellerId);
        Task<List<Message>> GetUserMessagesByListingIdAsync(int userId, int listingId);
        Task<List<Message>> GetLatestMessagesAsync(int userId);
        Task<Message> CreateNewConversationAsync(Message message);
        Task<Message> UpdateMessageAsync(int messageId, Message updatedMessage);
        Task<bool> DeleteMessageAsync(int messageId);
        Task<bool> DeleteConversationAsync(int userId, int sellerId);
    }
}