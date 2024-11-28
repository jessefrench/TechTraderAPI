using Moq;
using TechTrader.Models;
using TechTrader.Interfaces;
using TechTrader.Services;

namespace TechTrader.Tests
{
    public class MessageServiceTests
    {
        private readonly MessageService _messageService;
        private readonly Mock<IMessageRepository> _mockMessageRepository;

        public MessageServiceTests()
        {
            _mockMessageRepository = new Mock<IMessageRepository>();
            _messageService = new MessageService(_mockMessageRepository.Object);
        }

        [Fact]
        public async Task GetAllMessagesAsync_ShouldReturnListOfMessages()
        {
            // Arrange
            var userId = 1;

            var messages = new List<Message>
            {
                new Message
                {
                    Id = 1,
                    SenderId = userId,
                    ReceiverId = 2,
                    ListingId = 3,
                    Content = "Is this item still available?",
                    SentAt = DateTime.UtcNow.AddMinutes(-10)
                },
                new Message
                {
                    Id = 2,
                    SenderId = 2,
                    ReceiverId = userId,
                    ListingId = 3,
                    Content = "Yes, it's available!",
                    SentAt = DateTime.UtcNow.AddMinutes(-5)
                }
            };

            _mockMessageRepository
                .Setup(repo => repo.GetAllMessagesAsync(userId))
                .ReturnsAsync(messages);

            // Act
            var result = await _messageService.GetAllMessagesAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Message>>(result);
            Assert.Equal(messages.Count, result.Count);
            Assert.Collection(result,
                message =>
                {
                    Assert.Equal(messages[0].Id, message.Id);
                    Assert.Equal(messages[0].SenderId, message.SenderId);
                    Assert.Equal(messages[0].ReceiverId, message.ReceiverId);
                    Assert.Equal(messages[0].ListingId, message.ListingId);
                    Assert.Equal(messages[0].Content, message.Content);
                    Assert.Equal(messages[0].SentAt, message.SentAt);
                },
                message =>
                {
                    Assert.Equal(messages[1].Id, message.Id);
                    Assert.Equal(messages[1].SenderId, message.SenderId);
                    Assert.Equal(messages[1].ReceiverId, message.ReceiverId);
                    Assert.Equal(messages[1].ListingId, message.ListingId);
                    Assert.Equal(messages[1].Content, message.Content);
                    Assert.Equal(messages[1].SentAt, message.SentAt);
                });

            _mockMessageRepository.Verify(repo => repo.GetAllMessagesAsync(userId), Times.Once);
        }

        [Fact]
        public async Task CreateNewConversationAsync_ShouldReturnCreatedMessage()
        {
            // Arrange
            var newMessage = new Message
            {
                Id = 0, // Typically 0 for a new message before being saved
                SenderId = 1,
                ReceiverId = 2,
                ListingId = 3,
                Content = "Hi, is this item still available?",
                SentAt = DateTime.UtcNow
            };

            var createdMessage = new Message
            {
                Id = 1, // Simulate the ID generated after saving
                SenderId = 1,
                ReceiverId = 2,
                ListingId = 3,
                Content = "Hi, is this item still available?",
                SentAt = newMessage.SentAt
            };

            _mockMessageRepository
                .Setup(repo => repo.CreateNewConversationAsync(newMessage))
                .ReturnsAsync(createdMessage);

            // Act
            var result = await _messageService.CreateNewConversationAsync(newMessage);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Message>(result);
            Assert.Equal(createdMessage.Id, result.Id);
            Assert.Equal(newMessage.SenderId, result.SenderId);
            Assert.Equal(newMessage.ReceiverId, result.ReceiverId);
            Assert.Equal(newMessage.ListingId, result.ListingId);
            Assert.Equal(newMessage.Content, result.Content);
            Assert.Equal(newMessage.SentAt, result.SentAt);

            _mockMessageRepository.Verify(repo => repo.CreateNewConversationAsync(newMessage), Times.Once);
        }

        [Fact]
        public async Task UpdateMessageAsync_ShouldReturnUpdatedMessage()
        {
            // Arrange
            var messageId = 1;

            var existingMessage = new Message
            {
                Id = messageId,
                SenderId = 1,
                ReceiverId = 2,
                ListingId = 3,
                Content = "Hi, is this item still available?",
                SentAt = DateTime.UtcNow.AddMinutes(-10)
            };

            var updatedMessage = new Message
            {
                Id = messageId,
                SenderId = 1,
                ReceiverId = 2,
                ListingId = 3,
                Content = "Never mind, I am no longer interested.",
                SentAt = existingMessage.SentAt // SentAt remains unchanged
            };

            _mockMessageRepository
                .Setup(repo => repo.UpdateMessageAsync(messageId, updatedMessage))
                .ReturnsAsync(updatedMessage);

            // Act
            var result = await _messageService.UpdateMessageAsync(messageId, updatedMessage);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Message>(result);
            Assert.Equal(updatedMessage.Id, result.Id);
            Assert.Equal(updatedMessage.SenderId, result.SenderId);
            Assert.Equal(updatedMessage.ReceiverId, result.ReceiverId);
            Assert.Equal(updatedMessage.ListingId, result.ListingId);
            Assert.Equal(updatedMessage.Content, result.Content);
            Assert.Equal(updatedMessage.SentAt, result.SentAt);

            _mockMessageRepository.Verify(repo => repo.UpdateMessageAsync(messageId, updatedMessage), Times.Once);
        }

        [Fact]
        public async Task DeleteMessageAsync_ShouldReturnTrueWhenMessageIsDeleted()
        {
            // Arrange
            var messageId = 1;
            _mockMessageRepository
                .Setup(repo => repo.DeleteMessageAsync(messageId))
                .ReturnsAsync(true);

            // Act
            var result = await _messageService.DeleteMessageAsync(messageId);

            // Assert
            Assert.True(result);
            _mockMessageRepository.Verify(repo => repo.DeleteMessageAsync(messageId), Times.Once);
        }

        [Fact]
        public async Task DeleteMessageAsync_ShouldReturnFalseWhenMessageNotFound()
        {
            // Arrange
            var messageId = 99; // Non-existent message ID
            _mockMessageRepository
                .Setup(repo => repo.DeleteMessageAsync(messageId))
                .ReturnsAsync(false);

            // Act
            var result = await _messageService.DeleteMessageAsync(messageId);

            // Assert
            Assert.False(result);
            _mockMessageRepository.Verify(repo => repo.DeleteMessageAsync(messageId), Times.Once);
        }
    }
}