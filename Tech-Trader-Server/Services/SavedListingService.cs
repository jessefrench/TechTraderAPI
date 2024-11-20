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

        public async Task<List<SavedListing>> GetSavedListingsAsync()
        {
            return await _savedListingRepository.GetSavedListingsAsync();
        }

        public async Task<SavedListing> GetSavedListingByIdAsync(int savedListingId)
        {
            return await _savedListingRepository.GetSavedListingByIdAsync(savedListingId);
        }

        public async Task<SavedListing> CreateSavedListingAsync(SavedListing savedListing)
        {
            return await _savedListingRepository.CreateSavedListingAsync(savedListing);
        }

        public async Task<SavedListing> UpdateSavedListingAsync(int savedListingId, SavedListing savedListing)
        {
            return await _savedListingRepository.UpdateSavedListingAsync(savedListingId, savedListing);
        }

        public async Task<SavedListing> DeleteSavedListingAsync(int savedListingId)
        {
            return await _savedListingRepository.DeleteSavedListingAsync(savedListingId);
        }
    }
}