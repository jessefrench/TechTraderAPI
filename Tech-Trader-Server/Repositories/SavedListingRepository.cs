using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Repositories
{
    public class SavedListingRepository : ISavedListingRepository
    {
        private readonly TechTraderDbContext dbContext;

        public SavedListingRepository(TechTraderDbContext context)
        {
            dbContext = context;
        }

        // get all saved listings
        public async Task<List<SavedListing>> GetSavedListingsAsync()
        {
            return await dbContext.SavedListings.ToListAsync();
        }

        // get a single saved listing by id
        public async Task<SavedListing> GetSavedListingByIdAsync(int savedListingId)
        {
            SavedListing selectedSavedListing = await dbContext.SavedListings.FirstOrDefaultAsync(savedListing => savedListing.Id == savedListingId);
            return selectedSavedListing;
        }

        // create a saved listing
        public async Task<SavedListing> CreateSavedListingAsync(SavedListing savedListing)
        {
            await dbContext.SavedListings.AddAsync(savedListing);
            await dbContext.SaveChangesAsync();
            return savedListing;
        }

        // update a saved listing
        public async Task<SavedListing> UpdateSavedListingAsync(int savedListingId, SavedListing updatedSavedListing)
        {
            var savedListingToUpdate = await dbContext.SavedListings.FirstOrDefaultAsync(savedListing => savedListing.Id == savedListingId);

            if (savedListingToUpdate == null)
            {
                return null;
            }

            savedListingToUpdate.ListingId = updatedSavedListing.ListingId;

            await dbContext.SaveChangesAsync();
            return updatedSavedListing;
        }

        // delete a saved listing
        public async Task<SavedListing> DeleteSavedListingAsync(int savedListingId)
        {
            var savedListingToDelete = await dbContext.SavedListings.FirstOrDefaultAsync(savedListing => savedListing.Id == savedListingId);

            if (savedListingToDelete == null)
            {
                return null;
            }

            dbContext.SavedListings.Remove(savedListingToDelete);
            await dbContext.SaveChangesAsync();
            return savedListingToDelete;
        }
    }
}