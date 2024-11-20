using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _messageRepository.GetMessagesAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int messageId)
        {
            return await _messageRepository.GetMessageByIdAsync(messageId);
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            return await _messageRepository.CreateMessageAsync(message);
        }

        public async Task<Message> UpdateMessageAsync(int messageId, Message message)
        {
            return await _messageRepository.UpdateMessageAsync(messageId, message);
        }

        public async Task<Message> DeleteMessageAsync(int messageId)
        {
            return await _messageRepository.DeleteMessageAsync(messageId);
        }
    }
}