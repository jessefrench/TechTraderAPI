using TechTrader.Models;

namespace TechTrader.Data
{
    public class ListingData
    {
        public static List<Listing> Listings = new()
        {
            new Listing
            {
                Id = 1,
                SellerId = 1,
                CategoryId = 2,
                ConditionId = 1,
                Name = "MacBook Pro 16-inch",
                Description = "Brand new MacBook Pro with M1 chip, 16GB RAM, and 1TB SSD.",
                Price = 2200.00m,
                ImageUrl = "https://i.ebayimg.com/images/g/MWcAAOSwwNVjZsIZ/s-l400.jpg",
                CreatedOn = new DateTime(2024, 11, 15, 14, 30, 0),
                Sold = false,
            },
            new Listing
            {
                Id = 2,
                SellerId = 2,
                CategoryId = 5,
                ConditionId = 2,
                Name = "NVIDIA RTX 3080 GPU",
                Description = "Lightly used RTX 3080 graphics card, excellent condition.",
                Price = 650.00m,
                ImageUrl = "https://i.ebayimg.com/images/g/EEIAAOSw4ZhkRJyV/s-l400.jpg",
                CreatedOn = new DateTime(2024, 11, 14, 10, 0, 0),
                Sold = false,
            },
            new Listing
            {
                Id = 3,
                SellerId = 3,
                CategoryId = 8,
                ConditionId = 3,
                Name = "Call of Duty: Modern Warfare II",
                Description = "Open-box PS5 game, no scratches on the disc.",
                Price = 40.00m,
                ImageUrl = "https://i.ebayimg.com/images/g/qK8AAOSwdD9mcwe3/s-l400.png",
                CreatedOn = new DateTime(2024, 11, 12, 16, 45, 0),
                Sold = false,
            },
            new Listing
            {
                Id = 4,
                SellerId = 2,
                CategoryId = 6,
                ConditionId = 1,
                Name = "Razer BlackWidow Keyboard",
                Description = "New mechanical keyboard with RGB lighting.",
                Price = 120.00m,
                ImageUrl = "https://i.ebayimg.com/images/g/D0QAAOSwpGBm2-WD/s-l400.jpg",
                CreatedOn = new DateTime(2024, 11, 10, 11, 0, 0),
                Sold = true,
            },
            new Listing
            {
                Id = 5,
                SellerId = 1,
                CategoryId = 4,
                ConditionId = 2,
                Name = "PlayStation 5 Console",
                Description = "Used PS5 in great condition, comes with one controller.",
                Price = 450.00m,
                ImageUrl = "https://i.ebayimg.com/images/g/rf0AAOSwKeRlJhsW/s-l400.jpg",
                CreatedOn = new DateTime(2024, 11, 8, 9, 15, 0),
                Sold = false,
            }
        };
    }
}