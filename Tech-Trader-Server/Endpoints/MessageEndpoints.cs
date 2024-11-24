using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class MessageEndpoints
    {
        public static void Map(WebApplication app)
        {
            // get all user messages
            app.MapGet("/messages/{userId}", async (IMessageService messageService, int userId) =>
            {
                return await messageService.GetAllMessagesAsync(userId);
            })
            .Produces<List<Message>>(StatusCodes.Status200OK);

            // get a single message thread
            app.MapGet("/messages/{userId}/sellers/{sellerId}", async (IMessageService messageService, int userId, int sellerId) =>
            {
                return await messageService.GetSingleMessageThreadAsync(userId, sellerId);
            })
            .Produces<List<Message>>(StatusCodes.Status200OK);

            // get latest messages for each conversation
            app.MapGet("/messages/latest/{userId}", async (IMessageService messageService, int userId) =>
            {
                return await messageService.GetLatestMessagesAsync(userId);
            })
            .Produces<List<Message>>(StatusCodes.Status200OK);

            // create a new conversation
            app.MapPost("/messages", async (IMessageService messageService, Message message) =>
            {
                var newMessage = await messageService.CreateNewConversationAsync(message);
                return Results.Created($"/messages/{message.Id}", message);
            })
            .Produces<Message>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a message
            app.MapPut("/messages/{messageId}", async (IMessageService messageService, int messageId, Message updatedMessage) =>
            {
                var messageToUpdate = await messageService.UpdateMessageAsync(messageId, updatedMessage);
                return Results.Ok(messageToUpdate);
            })
            .Produces<Message>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

            // delete a message
            app.MapDelete("/messages/{messageId}", async (IMessageService messageService, int messageId) =>
            {
                var messageToDelete = await messageService.DeleteMessageAsync(messageId);
                return Results.NoContent();
            })
            .Produces<Message>(StatusCodes.Status204NoContent);

            // delete a conversation
            app.MapDelete("/messages/{userId}/sellers/{sellerId}", async (IMessageService messageService, int userId, int sellerId) =>
            {
                var conversationToDelete = await messageService.DeleteConversationAsync(userId, sellerId);
                return Results.NoContent();
            })
            .Produces<Message>(StatusCodes.Status204NoContent);
        }
    }
}