using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Models;
using MuseoAurora.Services;

namespace MuseoAurora.Endpoints
{
    public static class ExhibitionEndpoints
    {
        public static IEndpointRouteBuilder MapExhibitions(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/exhibitions/");
            group.MapGet("", GetExhibitions);
            group.MapGet("{id:int}", GetExhibitionById);
            group.MapPost("", CreateExhibition);
            group.MapPut("", UpdateExhibition);
            group.MapDelete("{id:int}", DeleteExhibitionById);
            return app;
        }

        public static async Task<Ok<IEnumerable<Exhibition>>> GetExhibitions(IExhibitionService service)
        {
            var items = await service.GetExhibitionsAsync();
            return TypedResults.Ok(items);
        }

        public static async Task<Results<NotFound, Ok<Exhibition>>> GetExhibitionById(IExhibitionService service, int id)
        {
            var item = await service.GetExhibitionByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            return TypedResults.Ok(item);
        }

        public static async Task<Results<BadRequest<string>, Ok<Exhibition>>> CreateExhibition(IExhibitionService service, Exhibition item)
        {
            if (item == null) return TypedResults.BadRequest("Invalid payload");
            var result = await service.CreateExhibitionAsync(item);
            if (!result.IsSuccess) return TypedResults.BadRequest(result.ErrorMessage!);
            return TypedResults.Ok(result.Data!);
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> UpdateExhibition(IExhibitionService service, Exhibition item)
        {
            var existingItem = await service.GetExhibitionByIdAsync(item.Id);
            if (existingItem == null) return TypedResults.NotFound();
            var success = await service.UpdateExhibitionAsync(item);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> DeleteExhibitionById(IExhibitionService service, int id)
        {
            var item = await service.GetExhibitionByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            var success = await service.DeleteExhibitionAsync(id);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }
    }
}