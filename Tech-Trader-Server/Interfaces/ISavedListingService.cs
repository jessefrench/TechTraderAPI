using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface ISavedListingService
    {
        Task<List<SavedListing>> GetSavedListingsByUserIdAsync(int userId);
        Task<IResult> AddSavedListingAsync(int listingId, int userId);
        Task<IResult> RemoveSavedListingAsync(int listingId, int userId);
    }
}