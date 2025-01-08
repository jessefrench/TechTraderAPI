using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<List<Listing>> GetListingsAsync()
        {
            return await _listingRepository.GetListingsAsync();
        }

        public async Task<List<Listing>> GetListingsBySellerIdAsync(int sellerId)
        {
            return await _listingRepository.GetListingsBySellerIdAsync(sellerId);
        }

        public async Task<Listing> GetListingByIdAsync(int listingId)
        {
            return await _listingRepository.GetListingByIdAsync(listingId);
        }

        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            return await _listingRepository.CreateListingAsync(listing);
        }

        public async Task<Listing> UpdateListingAsync(int listingId, Listing listing)
        {
            return await _listingRepository.UpdateListingAsync(listingId, listing);
        }

        public async Task<Listing> DeleteListingAsync(int listingId)
        {
            return await _listingRepository.DeleteListingAsync(listingId);
        }

        public async Task<List<Listing>> SearchListingsAsync(string searchValue)
        {
            return await _listingRepository.SearchListingsAsync(searchValue);
        }
    }
}