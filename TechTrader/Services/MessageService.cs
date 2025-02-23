using Microsoft.AspNetCore.SignalR;
using TechTrader.Models;
using TechTrader.Interfaces;
using TechTrader.Utility;

namespace TechTrader.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<List<Message>> GetAllMessagesAsync(int userId)
        {
            return await _messageRepository.GetAllMessagesAsync(userId);
        }

        public async Task<List<Message>> GetSingleMessageThreadAsync(int userId, int sellerId)
        {
            return await _messageRepository.GetSingleMessageThreadAsync(userId, sellerId);
        }

        public async Task<List<Message>> GetUserMessagesByListingIdAsync(int userId, int listingId)
        {
            return await _messageRepository.GetUserMessagesByListingIdAsync(userId, listingId);
        }

        public async Task<List<Message>> GetLatestMessagesAsync(int userId)
        {
            return await _messageRepository.GetLatestMessagesAsync(userId);
        }

        public async Task<Message> CreateNewConversationAsync(Message message, IHubContext<MessageHub> hubContext)
        {
            return await _messageRepository.CreateNewConversationAsync(message, hubContext);
        }

        public async Task<Message> UpdateMessageAsync(int messageId, Message updatedMessage)
        {
            return await _messageRepository.UpdateMessageAsync(messageId, updatedMessage);
        }

        public async Task<bool> DeleteMessageAsync(int messageId)
        {
            return await _messageRepository.DeleteMessageAsync(messageId);
        }

        public async Task<bool> DeleteConversationAsync(int userId, int sellerId)
        { 
            return await _messageRepository.DeleteConversationAsync(userId, sellerId);
        }
    }
}