using TechTrader.Models;

namespace TechTrader.Data
{
    public class SavedListingData
    {
        public static List<SavedListing> SavedListings = new()
        {
            new SavedListing
            {
                Id = 1,
                UserId = 2,
                ListingId = 1
            },
            new SavedListing
            {
                Id = 2,
                UserId = 3,
                ListingId = 2
            },
            new SavedListing
            {
                Id = 3,
                UserId = 1,
                ListingId = 3
            },
            new SavedListing
            {
                Id = 4,
                UserId = 2,
                ListingId = 5
            },
            new SavedListing
            {
                Id = 5,
                UserId = 3,
                ListingId = 4
            }
        };
    }
}