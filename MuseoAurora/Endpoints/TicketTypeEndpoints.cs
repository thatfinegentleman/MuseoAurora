using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Models;
using MuseoAurora.Services;

namespace MuseoAurora.Endpoints
{
    public static class TicketTypeEndpoints
    {
        public static IEndpointRouteBuilder MapTicketTypes(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/tickettypes/");
            group.MapGet("", GetTicketTypes);
            group.MapGet("{id:int}", GetTicketTypeById);
            group.MapPost("", CreateTicketType);
            group.MapPut("", UpdateTicketType);
            group.MapDelete("{id:int}", DeleteTicketTypeById);
            return app;
        }

        public static async Task<Ok<IEnumerable<TicketType>>> GetTicketTypes(ITicketTypeService service)
        {
            var items = await service.GetTicketTypesAsync();
            return TypedResults.Ok(items);
        }

        public static async Task<Results<NotFound, Ok<TicketType>>> GetTicketTypeById(ITicketTypeService service, int id)
        {
            var item = await service.GetTicketTypeByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            return TypedResults.Ok(item);
        }

        public static async Task<Results<BadRequest<string>, Ok<TicketType>>> CreateTicketType(ITicketTypeService service, TicketType item)
        {
            if (item == null) return TypedResults.BadRequest("Invalid payload");
            var result = await service.CreateTicketTypeAsync(item);
            if (!result.IsSuccess) return TypedResults.BadRequest(result.ErrorMessage!);
            return TypedResults.Ok(result.Data!);
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> UpdateTicketType(ITicketTypeService service, TicketType item)
        {
            var existingItem = await service.GetTicketTypeByIdAsync(item.Id);
            if (existingItem == null) return TypedResults.NotFound();
            var success = await service.UpdateTicketTypeAsync(item);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> DeleteTicketTypeById(ITicketTypeService service, int id)
        {
            var item = await service.GetTicketTypeByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            var success = await service.DeleteTicketTypeAsync(id);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }
    }
}