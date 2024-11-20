using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesAsync();
        Task<Message> GetMessageByIdAsync(int messageId);
        Task<Message> CreateMessageAsync(Message Message);
        Task<Message> UpdateMessageAsync(int messageId, Message Message);
        Task<Message> DeleteMessageAsync(int messageId);
    }
}