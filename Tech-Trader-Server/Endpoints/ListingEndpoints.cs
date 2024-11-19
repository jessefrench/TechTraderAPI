using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class ListingEndpoints
    {
        public static void Map(WebApplication app)
        {
            // get all listings
            app.MapGet("/listings", async (IListingService listingService) =>
            {
                return await listingService.GetListingsAsync();
            })
            .Produces<List<Listing>>(StatusCodes.Status200OK);

            // get listings by seller id
            app.MapGet("/listings/sellers/{sellerId}", async (IListingService listingService, int sellerId) =>
            {
                return await listingService.GetListingsBySellerIdAsync(sellerId);
            })
            .Produces<List<Listing>>(StatusCodes.Status200OK);

            // get a single listing by id
            app.MapGet("/listings{listingId}", async (IListingService listingService, int listingId) =>
            {
                Listing selectedListing = await listingService.GetListingByIdAsync(listingId);
                return Results.Ok(selectedListing);
            })
            .Produces<Listing>(StatusCodes.Status200OK);

            // create a new listing
            app.MapPost("/listings", async (IListingService listingService, Listing listing) =>
            {
                var newListing = await listingService.CreateListingAsync(listing);
                return Results.Created($"/listings/{listing.Id}", listing);
            })
            .Produces<Listing>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a listing
            app.MapPut("/listings/{listingId}", async (IListingService listingService, int listingId, Listing updatedListing) =>
            {
                var listingToUpdate = await listingService.UpdateListingAsync(listingId, updatedListing);
                return Results.Ok(listingToUpdate);
            })
            .Produces<Listing>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

            // delete a listing
            app.MapDelete("/listings/{listingId}", async (IListingService listingService, int listingId) =>
            {
                var listingToDelete = await listingService.DeleteListingAsync(listingId);
                return Results.NoContent();
            })
            .Produces<Listing>(StatusCodes.Status204NoContent);
        }
    }
}