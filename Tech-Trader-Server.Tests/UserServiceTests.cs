using Moq;
using TechTrader.Models;
using TechTrader.Interfaces;
using TechTrader.Services;

namespace TechTrader.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnUpdatedUser_WhenUserIsUpdatedSuccessfully()
        {
            // Arrange
            var userId = 1;
            var existingUser = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                City = "Old City",
                State = "Old State",
                Zip = "12345",
                IsSeller = false
            };

            var updatedUser = new User
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                City = "New City",
                State = "New State",
                Zip = "67890",
                IsSeller = true
            };

            _mockUserRepository
                .Setup(repo => repo.UpdateUserAsync(userId, updatedUser))
                .ReturnsAsync(updatedUser);

            // Act
            var result = await _userService.UpdateUserAsync(userId, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Id, result.Id);
            Assert.Equal(updatedUser.FirstName, result.FirstName);
            Assert.Equal(updatedUser.LastName, result.LastName);
            Assert.Equal(updatedUser.Email, result.Email);
            Assert.Equal(updatedUser.City, result.City);
            Assert.Equal(updatedUser.State, result.State);
            Assert.Equal(updatedUser.Zip, result.Zip);
            Assert.Equal(updatedUser.IsSeller, result.IsSeller);

            _mockUserRepository.Verify(repo => repo.UpdateUserAsync(userId, updatedUser), Times.Once);
        }
    }
}