using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Models;
using MuseoAurora.Services;

namespace MuseoAurora.Endpoints
{
    public static class GuidedTourEndpoints
    {
        public static IEndpointRouteBuilder MapGuidedTours(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/guidedtours/");
            group.MapGet("", GetGuidedTours);
            group.MapGet("{id:int}", GetGuidedTourById);
            group.MapPost("", CreateGuidedTour);
            group.MapPut("", UpdateGuidedTour);
            group.MapDelete("{id:int}", DeleteGuidedTourById);
            return app;
        }

        public static async Task<Ok<IEnumerable<GuidedTour>>> GetGuidedTours(IGuidedTourService service)
        {
            var items = await service.GetGuidedToursAsync();
            return TypedResults.Ok(items);
        }

        public static async Task<Results<NotFound, Ok<GuidedTour>>> GetGuidedTourById(IGuidedTourService service, int id)
        {
            var item = await service.GetGuidedTourByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            return TypedResults.Ok(item);
        }

        public static async Task<Results<BadRequest<string>, Ok<GuidedTour>>> CreateGuidedTour(IGuidedTourService service, GuidedTour item)
        {
            if (item == null) return TypedResults.BadRequest("Invalid payload");
            var result = await service.CreateGuidedTourAsync(item);
            if (!result.IsSuccess) return TypedResults.BadRequest(result.ErrorMessage!);
            return TypedResults.Ok(result.Data!);
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> UpdateGuidedTour(IGuidedTourService service, GuidedTour item)
        {
            var existingItem = await service.GetGuidedTourByIdAsync(item.Id);
            if (existingItem == null) return TypedResults.NotFound();
            var success = await service.UpdateGuidedTourAsync(item);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> DeleteGuidedTourById(IGuidedTourService service, int id)
        {
            var item = await service.GetGuidedTourByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            var success = await service.DeleteGuidedTourAsync(id);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }
    }
}