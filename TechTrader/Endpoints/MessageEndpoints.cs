using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public static class MessageEndpoints
    {
        public static void MapMessageEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/messages").WithTags(nameof(Message));

            // get all user messages
            group.MapGet("/{userId}", async (IMessageService messageService, int userId) =>
            {
                return await messageService.GetAllMessagesAsync(userId);
            })
            .WithName("GetAllMessages")
            .WithOpenApi()
            .Produces<List<Message>>(StatusCodes.Status200OK);

            // get a single message thread
            group.MapGet("/{userId}/sellers/{sellerId}", async (IMessageService messageService, int userId, int sellerId) =>
            {
                return await messageService.GetSingleMessageThreadAsync(userId, sellerId);
            })
            .WithName("GetSingleMessageThread")
            .WithOpenApi()
            .Produces<List<Message>>(StatusCodes.Status200OK);

            // get all messages for a specific listing
            group.MapGet("/{userId}/listings/{listingId}", async (IMessageService messageService, int userId, int listingId) =>
            {
                return await messageService.GetUserMessagesByListingIdAsync(userId, listingId);
            })
            .WithName("GetMessagesByListingId")
            .WithOpenApi()
            .Produces<List<Message>>(StatusCodes.Status200OK);

            // get latest messages for each conversation
            group.MapGet("/latest/{userId}", async (IMessageService messageService, int userId) =>
            {
                return await messageService.GetLatestMessagesAsync(userId);
            })
            .WithName("GetLatestMessages")
            .WithOpenApi()
            .Produces<List<Message>>(StatusCodes.Status200OK);

            // create a new conversation
            group.MapPost("/", async (IMessageService messageService, Message message) =>
            {
                var newMessage = await messageService.CreateNewConversationAsync(message);
                return Results.Created($"/messages/{message.Id}", message);
            })
            .WithName("CreateNewConversation")
            .WithOpenApi()
            .Produces<Message>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a message
            group.MapPut("/{messageId}", async (IMessageService messageService, int messageId, Message updatedMessage) =>
            {
                var messageToUpdate = await messageService.UpdateMessageAsync(messageId, updatedMessage);
                return Results.Ok(messageToUpdate);
            })
            .WithName("UpdateMessage")
            .WithOpenApi()
            .Produces<Message>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

            // delete a message
            group.MapDelete("/{messageId}", async (IMessageService messageService, int messageId) =>
            {
                var messageToDelete = await messageService.DeleteMessageAsync(messageId);
                return Results.NoContent();
            })
            .WithName("DeleteMessage")
            .WithOpenApi()
            .Produces<Message>(StatusCodes.Status204NoContent);

            // delete a conversation
            group.MapDelete("/{userId}/sellers/{sellerId}", async (IMessageService messageService, int userId, int sellerId) =>
            {
                var conversationToDelete = await messageService.DeleteConversationAsync(userId, sellerId);
                return Results.NoContent();
            })
            .WithName("DeleteConversation")
            .WithOpenApi()
            .Produces<Message>(StatusCodes.Status204NoContent);
        }
    }
}