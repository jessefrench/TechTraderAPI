using Moq;
using TechTrader.Models;
using TechTrader.Interfaces;
using TechTrader.Services;

namespace TechTrader.Tests
{
    public class ListingServiceTests
    {
        private readonly ListingService _listingService;
        private readonly Mock<IListingRepository> _mockListingRepository;

        public ListingServiceTests()
        {
            _mockListingRepository = new Mock<IListingRepository>();
            _listingService = new ListingService(_mockListingRepository.Object);
        }

        [Fact]
        public async Task GetListingsAsync_ShouldReturnListOfListings()
        {
            // Arrange
            var mockListings = new List<Listing>
            {
                new Listing
                {
                    Id = 1,
                    SellerId = 1,
                    CategoryId = 1,
                    ConditionId = 1,
                    Name = "Gaming Laptop",
                    Description = "A high-performance laptop for gaming.",
                    Price = 1500.00m,
                    ImageUrl = "http://example.com/gaming-laptop.jpg",
                    CreatedOn = DateTime.UtcNow,
                    Sold = false,
                    Seller = new User
                    {
                        Id = 1,
                        Uid = "user1",
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        City = "New York",
                        State = "NY",
                        Zip = "10001",
                        IsSeller = true,
                        ImageUrl = "http://example.com/john.jpg"
                    },
                    Category = new Category { Id = 1, Name = "Electronics" },
                    Condition = new Condition { Id = 1, Name = "New" }
                },
                new Listing
                {
                    Id = 2,
                    SellerId = 2,
                    CategoryId = 1,
                    ConditionId = 2,
                    Name = "Mechanical Keyboard",
                    Description = "RGB mechanical keyboard with blue switches.",
                    Price = 75.00m,
                    ImageUrl = "http://example.com/mechanical-keyboard.jpg",
                    CreatedOn = DateTime.UtcNow,
                    Sold = true,
                    Seller = new User
                    {
                        Id = 2,
                        Uid = "user2",
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        City = "Los Angeles",
                        State = "CA",
                        Zip = "90001",
                        IsSeller = true,
                        ImageUrl = "http://example.com/jane.jpg"
                    },
                    Category = new Category { Id = 1, Name = "Electronics" },
                    Condition = new Condition { Id = 2, Name = "Used" }
                }
            };

            _mockListingRepository
                .Setup(repo => repo.GetListingsAsync())
                .ReturnsAsync(mockListings);

            // Act
            var result = await _listingService.GetListingsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Listing>>(result);
            Assert.Equal(2, result.Count);

            // Validate the first listing
            var firstListing = result[0];
            Assert.Equal(1, firstListing.Id);
            Assert.Equal("Gaming Laptop", firstListing.Name);
            Assert.Equal("A high-performance laptop for gaming.", firstListing.Description);
            Assert.Equal(1500.00m, firstListing.Price);
            Assert.Equal("http://example.com/gaming-laptop.jpg", firstListing.ImageUrl);
            Assert.False(firstListing.Sold);
            Assert.Equal("John", firstListing.Seller.FirstName);
            Assert.Equal("Electronics", firstListing.Category.Name);
            Assert.Equal("New", firstListing.Condition.Name);

            // Validate the second listing
            var secondListing = result[1];
            Assert.Equal(2, secondListing.Id);
            Assert.Equal("Mechanical Keyboard", secondListing.Name);
            Assert.Equal("RGB mechanical keyboard with blue switches.", secondListing.Description);
            Assert.Equal(75.00m, secondListing.Price);
            Assert.Equal("http://example.com/mechanical-keyboard.jpg", secondListing.ImageUrl);
            Assert.True(secondListing.Sold);
            Assert.Equal("Jane", secondListing.Seller.FirstName);
            Assert.Equal("Electronics", secondListing.Category.Name);
            Assert.Equal("Used", secondListing.Condition.Name);

            _mockListingRepository.Verify(repo => repo.GetListingsAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateListingAsync_ShouldReturnCreatedListing()
        {
            // Arrange
            var newListing = new Listing
            {
                Id = 0, // Typically 0 for a new listing before being saved in the database
                SellerId = 1,
                CategoryId = 1,
                ConditionId = 1,
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse with Bluetooth.",
                Price = 25.99m,
                ImageUrl = "http://example.com/wireless-mouse.jpg",
                CreatedOn = DateTime.UtcNow,
                Sold = false
            };

            var createdListing = new Listing
            {
                Id = 3, // Simulate the ID generated after saving
                SellerId = 1,
                CategoryId = 1,
                ConditionId = 1,
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse with Bluetooth.",
                Price = 25.99m,
                ImageUrl = "http://example.com/wireless-mouse.jpg",
                CreatedOn = newListing.CreatedOn,
                Sold = false
            };

            _mockListingRepository
                .Setup(repo => repo.CreateListingAsync(newListing))
                .ReturnsAsync(createdListing);

            // Act
            var result = await _listingService.CreateListingAsync(newListing);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Listing>(result);
            Assert.Equal(3, result.Id);
            Assert.Equal(newListing.Name, result.Name);
            Assert.Equal(newListing.Description, result.Description);
            Assert.Equal(newListing.Price, result.Price);
            Assert.Equal(newListing.ImageUrl, result.ImageUrl);
            Assert.False(result.Sold);
            Assert.Equal(newListing.SellerId, result.SellerId);
            Assert.Equal(newListing.CategoryId, result.CategoryId);
            Assert.Equal(newListing.ConditionId, result.ConditionId);
            Assert.Equal(newListing.CreatedOn, result.CreatedOn);

            _mockListingRepository.Verify(repo => repo.CreateListingAsync(newListing), Times.Once);
        }

        [Fact]
        public async Task UpdateListingAsync_ShouldReturnUpdatedListing()
        {
            // Arrange
            var listingId = 1;
            var existingListing = new Listing
            {
                Id = listingId,
                SellerId = 1,
                CategoryId = 1,
                ConditionId = 1,
                Name = "Gaming Laptop",
                Description = "A high-performance laptop for gaming.",
                Price = 1500.00m,
                ImageUrl = "http://example.com/gaming-laptop.jpg",
                CreatedOn = DateTime.UtcNow,
                Sold = false
            };

            var updatedListing = new Listing
            {
                Id = listingId,
                SellerId = 1,
                CategoryId = 1,
                ConditionId = 2, // Updated condition
                Name = "Gaming Laptop - Updated",
                Description = "An updated description for the gaming laptop.",
                Price = 1400.00m, // Updated price
                ImageUrl = "http://example.com/gaming-laptop-updated.jpg", // Updated image URL
                CreatedOn = existingListing.CreatedOn, // Creation date remains the same
                Sold = false
            };

            _mockListingRepository
                .Setup(repo => repo.UpdateListingAsync(listingId, updatedListing))
                .ReturnsAsync(updatedListing);

            // Act
            var result = await _listingService.UpdateListingAsync(listingId, updatedListing);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Listing>(result);
            Assert.Equal(updatedListing.Id, result.Id);
            Assert.Equal(updatedListing.Name, result.Name);
            Assert.Equal(updatedListing.Description, result.Description);
            Assert.Equal(updatedListing.Price, result.Price);
            Assert.Equal(updatedListing.ImageUrl, result.ImageUrl);
            Assert.Equal(updatedListing.ConditionId, result.ConditionId);
            Assert.Equal(updatedListing.SellerId, result.SellerId);
            Assert.Equal(updatedListing.CategoryId, result.CategoryId);
            Assert.Equal(updatedListing.CreatedOn, result.CreatedOn);
            Assert.False(result.Sold);

            _mockListingRepository.Verify(repo => repo.UpdateListingAsync(listingId, updatedListing), Times.Once);
        }

        [Fact]
        public async Task DeleteListingAsync_ShouldReturnDeletedListing()
        {
            // Arrange
            var listingId = 1;
            var deletedListing = new Listing
            {
                Id = listingId,
                SellerId = 1,
                CategoryId = 1,
                ConditionId = 1,
                Name = "Wireless Headphones",
                Description = "Noise-cancelling wireless headphones.",
                Price = 200.00m,
                ImageUrl = "http://example.com/wireless-headphones.jpg",
                CreatedOn = DateTime.UtcNow,
                Sold = false
            };

            _mockListingRepository
                .Setup(repo => repo.DeleteListingAsync(listingId))
                .ReturnsAsync(deletedListing);

            // Act
            var result = await _listingService.DeleteListingAsync(listingId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Listing>(result);
            Assert.Equal(deletedListing.Id, result.Id);
            Assert.Equal(deletedListing.Name, result.Name);
            Assert.Equal(deletedListing.Description, result.Description);
            Assert.Equal(deletedListing.Price, result.Price);
            Assert.Equal(deletedListing.ImageUrl, result.ImageUrl);
            Assert.Equal(deletedListing.SellerId, result.SellerId);
            Assert.Equal(deletedListing.CategoryId, result.CategoryId);
            Assert.Equal(deletedListing.ConditionId, result.ConditionId);
            Assert.Equal(deletedListing.CreatedOn, result.CreatedOn);
            Assert.False(result.Sold);

            _mockListingRepository.Verify(repo => repo.DeleteListingAsync(listingId), Times.Once);
        }
    }
}