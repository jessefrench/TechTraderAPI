using Microsoft.AspNetCore.SignalR;
using TechTrader.Models;
using TechTrader.Utility;

namespace TechTrader.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllMessagesAsync(int userId);
        Task<List<Message>> GetSingleMessageThreadAsync(int userId, int sellerId);
        Task<List<Message>> GetUserMessagesByListingIdAsync(int userId, int listingId);
        Task<List<Message>> GetLatestMessagesAsync(int userId);
        Task<Message> CreateNewConversationAsync(Message message, IHubContext<MessageHub> hubContext);
        Task<Message> UpdateMessageAsync(int messageId, Message updatedMessage);
        Task<bool> DeleteMessageAsync(int messageId);
        Task<bool> DeleteConversationAsync(int userId, int sellerId);
    }
}