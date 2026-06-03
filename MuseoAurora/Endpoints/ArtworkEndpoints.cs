using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Models;
using MuseoAurora.Services;

namespace MuseoAurora.Endpoints
{
    public static class ArtworkEndpoints
    {
        public static IEndpointRouteBuilder MapArtworks(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/artworks/");
            group.MapGet("", GetArtworks);
            group.MapGet("{id:int}", GetArtworkById);
            group.MapPost("", CreateArtwork);
            group.MapPut("", UpdateArtwork);
            group.MapDelete("{id:int}", DeleteArtworkById);
            return app;
        }

        public static async Task<Ok<IEnumerable<Artwork>>> GetArtworks(IArtworkService service)
        {
            var items = await service.GetArtworksAsync();
            return TypedResults.Ok(items);
        }

        public static async Task<Results<NotFound, Ok<Artwork>>> GetArtworkById(IArtworkService service, int id)
        {
            var item = await service.GetArtworkByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            return TypedResults.Ok(item);
        }

        public static async Task<Results<BadRequest<string>, Ok<Artwork>>> CreateArtwork(IArtworkService service, Artwork item)
        {
            if (item == null) return TypedResults.BadRequest("Invalid payload");
            var result = await service.CreateArtworkAsync(item);
            if (!result.IsSuccess) return TypedResults.BadRequest(result.ErrorMessage!);
            return TypedResults.Ok(result.Data!);
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> UpdateArtwork(IArtworkService service, Artwork item)
        {
            var existingItem = await service.GetArtworkByIdAsync(item.Id);
            if (existingItem == null) return TypedResults.NotFound();
            var success = await service.UpdateArtworkAsync(item);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> DeleteArtworkById(IArtworkService service, int id)
        {
            var item = await service.GetArtworkByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            var success = await service.DeleteArtworkAsync(id);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }
    }
}