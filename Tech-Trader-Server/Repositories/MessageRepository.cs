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

        // get all messages
        public async Task<List<Message>> GetMessagesAsync()
        {
            return await dbContext.Messages.ToListAsync();
        }

        // get a single message by id
        public async Task<Message> GetMessageByIdAsync(int messageId)
        {
            Message selectedMessage = await dbContext.Messages.FirstOrDefaultAsync(message => message.Id == messageId);
            return selectedMessage;
        }

        // create a message
        public async Task<Message> CreateMessageAsync(Message message)
        {
            await dbContext.Messages.AddAsync(message);
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
        public async Task<Message> DeleteMessageAsync(int messageId)
        {
            var messageToDelete = await dbContext.Messages.FirstOrDefaultAsync(message => message.Id == messageId);

            if (messageToDelete == null)
            {
                return null;
            }

            dbContext.Messages.Remove(messageToDelete);
            await dbContext.SaveChangesAsync();
            return messageToDelete;
        }
    }
}