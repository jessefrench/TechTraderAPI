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
            return await dbContext.Listings.ToListAsync();
        }

        // get listings by seller id
        public async Task<List<Listing>> GetListingsBySellerIdAsync(int sellerId)
        {
            List<Listing> userListings = await dbContext.Listings.Where(listing => listing.SellerId == sellerId).ToListAsync();
            return userListings;
        }

        // get a single listing by id
        public async Task<Listing> GetListingByIdAsync(int listingId)
        {
            Listing selectedListing = await dbContext.Listings
                .Include(listing => listing.Category)
                .Include(listing => listing.Condition)
                .FirstOrDefaultAsync(listing =>  listing.Id == listingId);

            return selectedListing;
        }

        // create a listing
        public async Task<Listing> CreateListingAsync(Listing listing)
        {
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
    }
}