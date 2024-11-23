using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface ISavedListingRepository
    {
        Task<List<SavedListing>> GetSavedListingsByUserIdAsync(int userId);
        Task<IResult> AddSavedListingAsync(int listingId, int userId);
        Task<IResult> RemoveSavedListingAsync(int listingId, int userId);
    }
}