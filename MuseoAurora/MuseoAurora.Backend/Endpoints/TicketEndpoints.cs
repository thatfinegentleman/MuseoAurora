using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Backend.Services.Interfaces;
using MuseoAurora.Models;

namespace MuseoAurora.Backend.Endpoints
{
    public static class TicketEndpoints
    {
        public static IEndpointRouteBuilder MapTicketEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/tickets");

            group.MapGet("", GetTickets);
            group.MapGet("{id:int}", GetTicketById);
            group.MapPost("", AddTicket);
            group.MapPut("{id:int}", UpdateTicket);
            group.MapDelete("{id:int}", DeleteTicket);

            return app;
        }

        private static async Task<Ok<IEnumerable<Ticket>>> GetTickets(ITicketService service)
        {
            var list = await service.GetTicketsAsync();
            return TypedResults.Ok(list);
        }

        private static async Task<Results<NotFound, Ok<Ticket>>> GetTicketById(int id, ITicketService service)
        {
            var ticket = await service.GetTicketByIdAsync(id);
            return ticket == null ? TypedResults.NotFound() : TypedResults.Ok(ticket);
        }

        private static async Task<Results<Created<Ticket>, BadRequest>> AddTicket(Ticket ticket, ITicketService service)
        {
            var newTicket = await service.CreateTicketAsync(ticket);
            if (newTicket is null) return TypedResults.BadRequest();

            return TypedResults.Created($"/api/tickets/{newTicket.Id}", newTicket);
        }

        private static async Task<Results<NoContent, NotFound>> UpdateTicket(int id, Ticket ticket, ITicketService service)
        {
            ticket.Id = id;
            if (await service.UpdateTicketAsync(ticket))
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }

        private static async Task<Results<NoContent, NotFound>> DeleteTicket(int id, ITicketService service)
        {
            if (await service.DeleteTicketAsync(id))
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }
    }
}
