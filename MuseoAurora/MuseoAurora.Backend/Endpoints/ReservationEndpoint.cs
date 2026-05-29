using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Backend.Services.Interfaces;
using MuseoAurora.Models;

namespace MuseoAurora.Backend.Endpoints
{
    public static class ReservationEndpoints
    {
        public static IEndpointRouteBuilder MapReservationEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/reservations");

            group.MapGet("", GetReservations);
            group.MapGet("{id:int}", GetReservationById);
            group.MapPost("", AddReservation);
            group.MapPut("{id:int}", UpdateReservation);
            group.MapDelete("{id:int}", DeleteReservation);

            return app;
        }

        private static async Task<Ok<IEnumerable<Reservation>>> GetReservations(IReservationService service)
        {
            var list = await service.GetReservationsAsync();
            return TypedResults.Ok(list);
        }

        private static async Task<Results<NotFound, Ok<Reservation>>> GetReservationById(int id, IReservationService service)
        {
            var reservation = await service.GetReservationByIdAsync(id);
            return reservation == null ? TypedResults.NotFound() : TypedResults.Ok(reservation);
        }

        private static async Task<Results<Created<Reservation>, BadRequest>> AddReservation(Reservation reservation, IReservationService service)
        {
            var newReservation = await service.CreateReservationAsync(reservation);
            if (newReservation is null) return TypedResults.BadRequest();

            return TypedResults.Created($"/api/reservations/{newReservation.Id}", newReservation);
        }

        private static async Task<Results<NoContent, NotFound>> UpdateReservation(int id, Reservation reservation, IReservationService service)
        {
            reservation.Id = id;
            if (await service.UpdateReservationAsync(reservation))
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }

        private static async Task<Results<NoContent, NotFound>> DeleteReservation(int id, IReservationService service)
        {
            if (await service.DeleteReservationAsync(id))
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }
    }
}
