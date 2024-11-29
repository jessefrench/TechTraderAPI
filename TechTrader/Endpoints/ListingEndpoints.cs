using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public static class ListingEndpoints
    {
        public static void MapListingEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/listings").WithTags(nameof(Listing));

            // get all listings
            group.MapGet("/", async (IListingService listingService) =>
            {
                return await listingService.GetListingsAsync();
            })
            .WithName("GetListings")
            .WithOpenApi()
            .Produces<List<Listing>>(StatusCodes.Status200OK);

            // get listings by seller id
            group.MapGet("/sellers/{sellerId}", async (IListingService listingService, int sellerId) =>
            {
                return await listingService.GetListingsBySellerIdAsync(sellerId);
            })
            .WithName("GetListingsBySellerId")
            .WithOpenApi()
            .Produces<List<Listing>>(StatusCodes.Status200OK);

            // get a single listing by id
            group.MapGet("/{listingId}", async (IListingService listingService, int listingId) =>
            {
                Listing selectedListing = await listingService.GetListingByIdAsync(listingId);
                return Results.Ok(selectedListing);
            })
            .WithName("GetListingById")
            .WithOpenApi()
            .Produces<Listing>(StatusCodes.Status200OK);

            // create a new listing
            group.MapPost("/", async (IListingService listingService, Listing listing) =>
            {
                var newListing = await listingService.CreateListingAsync(listing);
                return Results.Created($"/listings/{listing.Id}", listing);
            })
            .WithName("CreateListing")
            .WithOpenApi()
            .Produces<Listing>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a listing
            group.MapPut("/{listingId}", async (IListingService listingService, int listingId, Listing updatedListing) =>
            {
                var listingToUpdate = await listingService.UpdateListingAsync(listingId, updatedListing);
                return Results.Ok(listingToUpdate);
            })
            .WithName("UpdateListing")
            .WithOpenApi()
            .Produces<Listing>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

            // delete a listing
            group.MapDelete("/{listingId}", async (IListingService listingService, int listingId) =>
            {
                var listingToDelete = await listingService.DeleteListingAsync(listingId);
                return Results.NoContent();
            })
            .WithName("DeleteListing")
            .WithOpenApi()
            .Produces<Listing>(StatusCodes.Status204NoContent);
        }
    }
}