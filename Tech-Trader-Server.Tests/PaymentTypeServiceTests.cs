using Moq;
using Microsoft.AspNetCore.Http;
using TechTrader.Models;
using TechTrader.Interfaces;
using TechTrader.Services;

namespace TechTrader.Tests
{
    public class PaymentTypeServiceTests
    {
        private readonly PaymentTypeService _paymentTypeService;
        private readonly Mock<IPaymentTypeRepository> _mockPaymentTypeRepository;

        public PaymentTypeServiceTests()
        {
            _mockPaymentTypeRepository = new Mock<IPaymentTypeRepository>();
            _paymentTypeService = new PaymentTypeService(_mockPaymentTypeRepository.Object);
        }

        [Fact]
        public async Task GetPaymentTypesAsync_ShouldReturnListOfPaymentTypes()
        {
            // Arrange
            var paymentTypes = new List<PaymentType>
            {
                new PaymentType
                {
                    Id = 1,
                    Name = "Credit Card",
                    Users = new List<User>
                    {
                        new User { Id = 1, FirstName = "John", LastName = "Doe" },
                        new User { Id = 2, FirstName = "Jane", LastName = "Doe" }
                    }
                },
                new PaymentType
                {
                    Id = 2,
                    Name = "PayPal",
                    Users = new List<User>
                    {
                        new User { Id = 3, FirstName = "Alice", LastName = "Smith" }
                    }
                }
            };

            _mockPaymentTypeRepository
                .Setup(repo => repo.GetPaymentTypesAsync())
                .ReturnsAsync(paymentTypes);

            // Act
            var result = await _paymentTypeService.GetPaymentTypesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PaymentType>>(result);
            Assert.Equal(paymentTypes.Count, result.Count);
            Assert.Collection(result,
                paymentType =>
                {
                    Assert.Equal(paymentTypes[0].Id, paymentType.Id);
                    Assert.Equal(paymentTypes[0].Name, paymentType.Name);
                    Assert.NotNull(paymentType.Users);
                    Assert.Equal(paymentTypes[0].Users.Count, paymentType.Users.Count);
                },
                paymentType =>
                {
                    Assert.Equal(paymentTypes[1].Id, paymentType.Id);
                    Assert.Equal(paymentTypes[1].Name, paymentType.Name);
                    Assert.NotNull(paymentType.Users);
                    Assert.Equal(paymentTypes[1].Users.Count, paymentType.Users.Count);
                });

            _mockPaymentTypeRepository.Verify(repo => repo.GetPaymentTypesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddPaymentTypeToUserAsync_ShouldReturnSuccessResult_WhenPaymentTypeIsAddedToUser()
        {
            // Arrange
            var paymentTypeId = 1;
            var userId = 100;
            var successResult = Results.Ok("Payment type added to user successfully.");

            _mockPaymentTypeRepository
                .Setup(repo => repo.AddPaymentTypeToUserAsync(paymentTypeId, userId))
                .ReturnsAsync(successResult);

            // Act
            var result = await _paymentTypeService.AddPaymentTypeToUserAsync(paymentTypeId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(successResult, result);
            _mockPaymentTypeRepository.Verify(repo => repo.AddPaymentTypeToUserAsync(paymentTypeId, userId), Times.Once);
        }

        [Fact]
        public async Task AddPaymentTypeToUserAsync_ShouldReturnFailureResult_WhenPaymentTypeAlreadyExistsForUser()
        {
            // Arrange
            var paymentTypeId = 1;
            var userId = 100;
            var failureResult = Results.BadRequest("Payment type already exists for this user.");

            _mockPaymentTypeRepository
                .Setup(repo => repo.AddPaymentTypeToUserAsync(paymentTypeId, userId))
                .ReturnsAsync(failureResult);

            // Act
            var result = await _paymentTypeService.AddPaymentTypeToUserAsync(paymentTypeId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(failureResult, result);
            _mockPaymentTypeRepository.Verify(repo => repo.AddPaymentTypeToUserAsync(paymentTypeId, userId), Times.Once);
        }

        [Fact]
        public async Task RemovePaymentTypeFromUserAsync_ShouldReturnSuccessResult_WhenPaymentTypeIsRemovedFromUser()
        {
            // Arrange
            var paymentTypeId = 1;
            var userId = 100;
            var successResult = Results.Ok("Payment type removed from user successfully.");

            _mockPaymentTypeRepository
                .Setup(repo => repo.RemovePaymentTypeFromUserAsync(paymentTypeId, userId))
                .ReturnsAsync(successResult);

            // Act
            var result = await _paymentTypeService.RemovePaymentTypeFromUserAsync(paymentTypeId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(successResult, result);
            _mockPaymentTypeRepository.Verify(repo => repo.RemovePaymentTypeFromUserAsync(paymentTypeId, userId), Times.Once);
        }

        [Fact]
        public async Task RemovePaymentTypeFromUserAsync_ShouldReturnFailureResult_WhenPaymentTypeDoesNotExistForUser()
        {
            // Arrange
            var paymentTypeId = 1;
            var userId = 100;
            var failureResult = Results.NotFound("Payment type not found for the specified user.");

            _mockPaymentTypeRepository
                .Setup(repo => repo.RemovePaymentTypeFromUserAsync(paymentTypeId, userId))
                .ReturnsAsync(failureResult);

            // Act
            var result = await _paymentTypeService.RemovePaymentTypeFromUserAsync(paymentTypeId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(failureResult, result);
            _mockPaymentTypeRepository.Verify(repo => repo.RemovePaymentTypeFromUserAsync(paymentTypeId, userId), Times.Once);
        }
    }
}