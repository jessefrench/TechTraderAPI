using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class SavedListingEndpoints
    {
        public static void Map(WebApplication app)
        {
            // get all saved listings
            app.MapGet("/saved-listings", async (ISavedListingService savedListingService) =>
            {
                return await savedListingService.GetSavedListingsAsync();
            })
            .Produces<List<SavedListing>>(StatusCodes.Status200OK);

            // get a single saved listing by id
            app.MapGet("/saved-listings/{savedListingId}", async (ISavedListingService savedListingService, int savedListingId) =>
            {
                SavedListing selectedSavedListing = await savedListingService.GetSavedListingByIdAsync(savedListingId);
                return Results.Ok(selectedSavedListing);
            })
            .Produces<SavedListing>(StatusCodes.Status200OK);

            // create a new saved listing
            app.MapPost("/saved-listings", async (ISavedListingService savedListingService, SavedListing savedListing) =>
            {
                var newSavedListing = await savedListingService.CreateSavedListingAsync(savedListing);
                return Results.Created($"/savedListings/{savedListing.Id}", savedListing);
            })
            .Produces<SavedListing>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a saved listing
            app.MapPut("/saved-listings/{savedListingId}", async (ISavedListingService savedListingService, int savedListingId, SavedListing updatedSavedListing) =>
            {
                var savedListingToUpdate = await savedListingService.UpdateSavedListingAsync(savedListingId, updatedSavedListing);
                return Results.Ok(savedListingToUpdate);
            })
            .Produces<SavedListing>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

            // delete a saved listing
            app.MapDelete("/saved-listings/{savedListingId}", async (ISavedListingService savedListingService, int savedListingId) =>
            {
                var savedListingToDelete = await savedListingService.DeleteSavedListingAsync(savedListingId);
                return Results.NoContent();
            })
            .Produces<SavedListing>(StatusCodes.Status204NoContent);
        }
    }
}