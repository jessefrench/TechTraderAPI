using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly TechTraderDbContext dbContext;

        public ListingRepository(TechTraderDbContext context)
        {
            dbContext = context;
        }

        // get all listings
        public async Task<List<Listing>> GetListingsAsync()
        {
            return await dbContext.Listings
                .Include(listing => listing.Seller)
                .Include(listing => listing.Category)
                .Include(listing => listing.Condition)
                .ToListAsync();
        }

        // get listings by seller id
        public async Task<List<Listing>> GetListingsBySellerIdAsync(int sellerId)
        {
            List<Listing> userListings = await dbContext.Listings
                .Include(listing => listing.Seller)
                .Include(listing => listing.Category)
                .Include(listing => listing.Condition)
                .Where(listing => listing.SellerId == sellerId).ToListAsync();

            return userListings;
        }

        // get a single listing by id
        public async Task<Listing> GetListingByIdAsync(int listingId)
        {
            Listing selectedListing = await dbContext.Listings
                .Include(listing => listing.Seller)
                .Include(listing => listing.Category)
                .Include(listing => listing.Condition)
                .FirstOrDefaultAsync(listing =>  listing.Id == listingId);

            return selectedListing;
        }

        // create a listing
        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            listing.CreatedOn = DateTime.UtcNow;
            await dbContext.Listings.AddAsync(listing);
            await dbContext.SaveChangesAsync();
            return listing;
        }

        // update a listing
        public async Task<Listing> UpdateListingAsync(int listingId, Listing updatedListing)
        {
            var listingToUpdate = await dbContext.Listings.FirstOrDefaultAsync(listing => listing.Id == listingId);

            if (listingToUpdate == null)
            {
                return null;
            }

            listingToUpdate.Name = updatedListing.Name;
            listingToUpdate.Description = updatedListing.Description;
            listingToUpdate.Price = updatedListing.Price;
            listingToUpdate.CategoryId = updatedListing.CategoryId;
            listingToUpdate.ConditionId = updatedListing.ConditionId;
            listingToUpdate.ImageUrl = updatedListing.ImageUrl;
            listingToUpdate.Sold = updatedListing.Sold;

            await dbContext.SaveChangesAsync();
            return updatedListing;
        }

        // delete a listing
        public async Task<Listing> DeleteListingAsync(int listingId)
        {
            var listingToDelete = await dbContext.Listings.FirstOrDefaultAsync(listing => listing.Id == listingId);

            if (listingToDelete == null)
            {
                return null;
            }

            dbContext.Listings.Remove(listingToDelete);
            await dbContext.SaveChangesAsync();
            return listingToDelete;
        }

        // search listings
        public async Task<List<Listing>> SearchListingsAsync(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return new List<Listing>();
            }

            var searchLower = searchValue.ToLowerInvariant();

            return await dbContext.Listings
                .Include(listing => listing.Seller)
                .Include(listing => listing.Category)
                .Include(listing => listing.Condition)
                .Where(listing =>
                    listing.Name.ToLower().Contains(searchLower) ||
                    listing.Description.ToLower().Contains(searchLower) ||
                    (listing.Category != null && listing.Category.Name.ToLower().Contains(searchLower)) ||
                    (listing.Condition != null && listing.Condition.Name.ToLower().Contains(searchLower)) ||
                    listing.Seller.City.ToLower().Contains(searchLower) ||
                    listing.Seller.FirstName.ToLower().Contains(searchLower) ||
                    listing.Seller.LastName.ToLower().Contains(searchLower))
                .ToListAsync();
        }
    }
}