using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Services
{
    public class SavedListingService : ISavedListingService
    {
        private readonly ISavedListingRepository _savedListingRepository;

        public SavedListingService(ISavedListingRepository savedListingRepository)
        {
            _savedListingRepository = savedListingRepository;
        }

        public async Task<List<SavedListing>> GetSavedListingsByUserIdAsync(int userId)
        {
            return await _savedListingRepository.GetSavedListingsByUserIdAsync(userId);
        }

        public async Task<IResult> AddSavedListingAsync(int listingId, int userId)
        {
            return await _savedListingRepository.AddSavedListingAsync(listingId, userId);
        }

        public async Task<IResult> RemoveSavedListingAsync(int listingId, int userId)
        {
            return await _savedListingRepository.RemoveSavedListingAsync(listingId, userId);
        }
    }
}