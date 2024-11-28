using Moq;
using Microsoft.AspNetCore.Http;
using TechTrader.Models;
using TechTrader.Interfaces;
using TechTrader.Services;

namespace TechTrader.Tests
{
    public class SavedListingServiceTests
    {
        private readonly SavedListingService _savedListingService;
        private readonly Mock<ISavedListingRepository> _mockSavedListingRepository;

        public SavedListingServiceTests()
        {
            _mockSavedListingRepository = new Mock<ISavedListingRepository>();
            _savedListingService = new SavedListingService(_mockSavedListingRepository.Object);
        }

        [Fact]
        public async Task GetSavedListingsByUserIdAsync_ShouldReturnListOfSavedListings()
        {
            // Arrange
            var userId = 1;

            var savedListings = new List<SavedListing>
            {
                new SavedListing
                {
                    Id = 1,
                    UserId = userId,
                    ListingId = 100,
                    Listing = new Listing
                    {
                        Id = 100,
                        SellerId = 2,
                        CategoryId = 1,
                        ConditionId = 3,
                        Name = "Gaming Laptop",
                        Description = "High-performance gaming laptop.",
                        Price = 1500.00m,
                        ImageUrl = "http://example.com/laptop.jpg",
                        CreatedOn = DateTime.UtcNow.AddDays(-5),
                        Sold = false
                    }
                },
                new SavedListing
                {
                    Id = 2,
                    UserId = userId,
                    ListingId = 101,
                    Listing = new Listing
                    {
                        Id = 101,
                        SellerId = 3,
                        CategoryId = 2,
                        ConditionId = 1,
                        Name = "Mechanical Keyboard",
                        Description = "RGB mechanical keyboard.",
                        Price = 80.00m,
                        ImageUrl = "http://example.com/keyboard.jpg",
                        CreatedOn = DateTime.UtcNow.AddDays(-2),
                        Sold = false
                    }
                }
            };

            _mockSavedListingRepository
                .Setup(repo => repo.GetSavedListingsByUserIdAsync(userId))
                .ReturnsAsync(savedListings);

            // Act
            var result = await _savedListingService.GetSavedListingsByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<SavedListing>>(result);
            Assert.Equal(savedListings.Count, result.Count);
            Assert.Collection(result,
                savedListing =>
                {
                    Assert.Equal(savedListings[0].Id, savedListing.Id);
                    Assert.Equal(savedListings[0].UserId, savedListing.UserId);
                    Assert.Equal(savedListings[0].ListingId, savedListing.ListingId);
                    Assert.NotNull(savedListing.Listing);
                    Assert.Equal(savedListings[0].Listing.Name, savedListing.Listing.Name);
                },
                savedListing =>
                {
                    Assert.Equal(savedListings[1].Id, savedListing.Id);
                    Assert.Equal(savedListings[1].UserId, savedListing.UserId);
                    Assert.Equal(savedListings[1].ListingId, savedListing.ListingId);
                    Assert.NotNull(savedListing.Listing);
                    Assert.Equal(savedListings[1].Listing.Name, savedListing.Listing.Name);
                });

            _mockSavedListingRepository.Verify(repo => repo.GetSavedListingsByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task AddSavedListingAsync_ShouldReturnSuccessResult_WhenListingIsAdded()
        {
            // Arrange
            var listingId = 100;
            var userId = 1;
            var successResult = Results.Ok("Saved listing added successfully.");

            _mockSavedListingRepository
                .Setup(repo => repo.AddSavedListingAsync(listingId, userId))
                .ReturnsAsync(successResult);

            // Act
            var result = await _savedListingService.AddSavedListingAsync(listingId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(successResult, result);
            _mockSavedListingRepository.Verify(repo => repo.AddSavedListingAsync(listingId, userId), Times.Once);
        }

        [Fact]
        public async Task AddSavedListingAsync_ShouldReturnFailureResult_WhenListingAlreadySaved()
        {
            // Arrange
            var listingId = 100;
            var userId = 1;
            var failureResult = Results.BadRequest("Listing is already saved.");

            _mockSavedListingRepository
                .Setup(repo => repo.AddSavedListingAsync(listingId, userId))
                .ReturnsAsync(failureResult);

            // Act
            var result = await _savedListingService.AddSavedListingAsync(listingId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(failureResult, result);
            _mockSavedListingRepository.Verify(repo => repo.AddSavedListingAsync(listingId, userId), Times.Once);
        }

        [Fact]
        public async Task RemoveSavedListingAsync_ShouldReturnSuccessResult_WhenListingIsRemoved()
        {
            // Arrange
            var listingId = 100;
            var userId = 1;
            var successResult = Results.Ok("Saved listing removed successfully.");

            _mockSavedListingRepository
                .Setup(repo => repo.RemoveSavedListingAsync(listingId, userId))
                .ReturnsAsync(successResult);

            // Act
            var result = await _savedListingService.RemoveSavedListingAsync(listingId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(successResult, result);
            _mockSavedListingRepository.Verify(repo => repo.RemoveSavedListingAsync(listingId, userId), Times.Once);
        }

        [Fact]
        public async Task RemoveSavedListingAsync_ShouldReturnFailureResult_WhenListingDoesNotExist()
        {
            // Arrange
            var listingId = 200; // Non-existent listing
            var userId = 1;
            var failureResult = Results.NotFound("Saved listing not found.");

            _mockSavedListingRepository
                .Setup(repo => repo.RemoveSavedListingAsync(listingId, userId))
                .ReturnsAsync(failureResult);

            // Act
            var result = await _savedListingService.RemoveSavedListingAsync(listingId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(failureResult, result);
            _mockSavedListingRepository.Verify(repo => repo.RemoveSavedListingAsync(listingId, userId), Times.Once);
        }
    }
}