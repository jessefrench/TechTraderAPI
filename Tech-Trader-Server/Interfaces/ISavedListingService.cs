using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface ISavedListingService
    {
        Task<List<SavedListing>> GetSavedListingsAsync();
        Task<SavedListing> GetSavedListingByIdAsync(int savedListingId);
        Task<SavedListing> CreateSavedListingAsync(SavedListing SavedListing);
        Task<SavedListing> UpdateSavedListingAsync(int savedListingId, SavedListing SavedListing);
        Task<SavedListing> DeleteSavedListingAsync(int savedListingId);
    }
}