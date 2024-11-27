using Moq;
using TechTrader.Models;
using TechTrader.Interfaces;
using TechTrader.Services;

namespace TechTrader.Tests
{
    public class CategoryServiceTests
    {
        private readonly CategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldReturnUpdatedCategory_WhenCategoryIsUpdatedSuccessfully()
        {
            // Arrange
            var categoryId = 1;
            var existingCategory = new Category
            {
                Id = categoryId,
                Name = "Old Category Name"
            };

            var updatedCategory = new Category
            {
                Id = categoryId,
                Name = "New Category Name"
            };

            _mockCategoryRepository
                .Setup(repo => repo.UpdateCategoryAsync(categoryId, updatedCategory))
                .ReturnsAsync(updatedCategory);

            // Act
            var result = await _categoryService.UpdateCategoryAsync(categoryId, updatedCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedCategory.Id, result.Id);
            Assert.Equal(updatedCategory.Name, result.Name);

            _mockCategoryRepository.Verify(repo => repo.UpdateCategoryAsync(categoryId, updatedCategory), Times.Once);
        }
    }
}