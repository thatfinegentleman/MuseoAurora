using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Backend.Services;
using MuseoAurora.Models;

namespace MuseoAurora.Backend.Endpoints
{
    public static class ArtworkEndpoints
    {
        public static IEndpointRouteBuilder MapArtworkEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/artworks");

            group.MapGet("", GetArtworks);
            group.MapGet("{id:int}", GetArtworkById);
            group.MapPost("", AddArtwork);
            group.MapPut("{id:int}", UpdateArtwork);
            group.MapDelete("{id:int}", DeleteArtwork);

            return app;
        }

        // GET: api/artworks
        private static async Task<Ok<IEnumerable<Artwork>>> GetArtworks(IArtworkService artworkService)
        {
            var list = await artworkService.GetArtworksAsync();
            return TypedResults.Ok(list); 
        }

        // GET: api/artworks/{id}
        private static async Task<Results<NotFound, Ok<Artwork>>> GetArtworkById(int id, IArtworkService artworkService)
        {
            var artwork = await artworkService.GetArtworkByIdAsync(id);

            if (artwork == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(artwork); 
        }

        // POST: api/artworks
        private static async Task<Results<Created<Artwork>, BadRequest>> AddArtwork(Artwork artwork, IArtworkService artworkService)
        {
            var newArtwork = await artworkService.CreateArtworkAsync(artwork);

            if (newArtwork is null)
            {
                return TypedResults.BadRequest();
            }
            return TypedResults.Created($"/api/artworks/{newArtwork.Id}", newArtwork);
        }

        // PUT: api/artworks/{id}
        private static async Task<Results<NoContent, NotFound>> UpdateArtwork(int id, Artwork artwork, IArtworkService artworkService)
        {
            artwork.Id = id;
            if (await artworkService.UpdateArtworkAsync(artwork))
            {
                return TypedResults.NoContent(); 
            }
            else
            {
                return TypedResults.NotFound();
            }
        }

        // DELETE: api/artworks/{id}
        private static async Task<Results<NoContent, NotFound>> DeleteArtwork(int id, IArtworkService artworkService)
        {
            if (await artworkService.DeleteArtworkAsync(id))
            {
                return TypedResults.NoContent(); // 204 No Content
            }
            else
            {
                return TypedResults.NotFound(); // 404 Not Found
            }
        }
    }
}
