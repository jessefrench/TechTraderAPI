using TechTrader.Models;

namespace TechTrader.Data
{
    public class CategoryData
    {
        public static List<Category> Categories = new()
        {
            new Category { Id = 1, Name = "Desktops" },
            new Category { Id = 2, Name = "Laptops" },
            new Category { Id = 3, Name = "Monitors" },
            new Category { Id = 4, Name = "Gaming Consoles" },
            new Category { Id = 5, Name = "PC Parts" },
            new Category { Id = 6, Name = "PC Accessories" },
            new Category { Id = 7, Name = "Gaming Console Accessories" },
            new Category { Id = 8, Name = "Video Games" },
            new Category { Id = 9, Name = "Movies" },
            new Category { Id = 10, Name = "TV Shows" }
        };
    }
}