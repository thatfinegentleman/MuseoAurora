using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Models;
using MuseoAurora.Services;

namespace MuseoAurora.Endpoints
{
    public static class TicketEndpoints
    {
        public static IEndpointRouteBuilder MapTickets(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/tickets/");
            group.MapGet("", GetTickets);
            group.MapGet("{id:int}", GetTicketById);
            group.MapPost("", CreateTicket);
            group.MapPut("", UpdateTicket);
            group.MapDelete("{id:int}", DeleteTicketById);
            return app;
        }

        public static async Task<Ok<IEnumerable<Ticket>>> GetTickets(ITicketService service)
        {
            var items = await service.GetTicketsAsync();
            return TypedResults.Ok(items);
        }

        public static async Task<Results<NotFound, Ok<Ticket>>> GetTicketById(ITicketService service, int id)
        {
            var item = await service.GetTicketByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            return TypedResults.Ok(item);
        }

        public static async Task<Results<BadRequest<string>, Ok<Ticket>>> CreateTicket(ITicketService service, Ticket item)
        {
            if (item == null) return TypedResults.BadRequest("Invalid payload");
            var result = await service.CreateTicketAsync(item);
            if (!result.IsSuccess) return TypedResults.BadRequest(result.ErrorMessage!);
            return TypedResults.Ok(result.Data!);
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> UpdateTicket(ITicketService service, Ticket item)
        {
            var existingItem = await service.GetTicketByIdAsync(item.Id);
            if (existingItem == null) return TypedResults.NotFound();
            var success = await service.UpdateTicketAsync(item);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> DeleteTicketById(ITicketService service, int id)
        {
            var item = await service.GetTicketByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            var success = await service.DeleteTicketAsync(id);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }
    }
}