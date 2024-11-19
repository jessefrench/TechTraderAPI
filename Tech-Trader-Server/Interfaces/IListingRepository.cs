using TechTrader.Models;

namespace TechTrader.Interfaces
{
    public interface IListingRepository
    {
        Task<List<Listing>> GetListingsAsync();
        Task<List<Listing>> GetListingsBySellerIdAsync(int sellerId);
        Task<Listing> GetListingByIdAsync(int listingId);
        Task<Listing> CreateListingAsync(Listing Listing);
        Task<Listing> UpdateListingAsync(int listingId, Listing Listing);
        Task<Listing> DeleteListingAsync(int listingId);
    }
}