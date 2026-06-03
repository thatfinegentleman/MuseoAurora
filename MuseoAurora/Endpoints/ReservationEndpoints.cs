using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Models;
using MuseoAurora.Services;

namespace MuseoAurora.Endpoints
{
    public static class ReservationEndpoints
    {
        public static IEndpointRouteBuilder MapReservations(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/reservations/");
            group.MapGet("", GetReservations);
            group.MapGet("{id:int}", GetReservationById);
            group.MapPost("", CreateReservation);
            group.MapPut("", UpdateReservation);
            group.MapDelete("{id:int}", DeleteReservationById);
            return app;
        }

        public static async Task<Ok<IEnumerable<Reservation>>> GetReservations(IReservationService service)
        {
            var items = await service.GetReservationsAsync();
            return TypedResults.Ok(items);
        }

        public static async Task<Results<NotFound, Ok<Reservation>>> GetReservationById(IReservationService service, int id)
        {
            var item = await service.GetReservationByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            return TypedResults.Ok(item);
        }

        public static async Task<Results<BadRequest<string>, Ok<Reservation>>> CreateReservation(IReservationService service, Reservation item)
        {
            if (item == null) return TypedResults.BadRequest("Invalid payload");
            var result = await service.CreateReservationAsync(item);
            if (!result.IsSuccess) return TypedResults.BadRequest(result.ErrorMessage!);
            return TypedResults.Ok(result.Data!);
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> UpdateReservation(IReservationService service, Reservation item)
        {
            var existingItem = await service.GetReservationByIdAsync(item.Id);
            if (existingItem == null) return TypedResults.NotFound();
            var success = await service.UpdateReservationAsync(item);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> DeleteReservationById(IReservationService service, int id)
        {
            var item = await service.GetReservationByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            var success = await service.DeleteReservationAsync(id);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }
    }
}