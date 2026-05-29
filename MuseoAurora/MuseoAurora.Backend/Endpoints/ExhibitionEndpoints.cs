using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Backend.Services.Interfaces;
using MuseoAurora.Models;

namespace MuseoAurora.Backend.Endpoints
{
    public static class ExhibitionEndpoints
    {
        public static IEndpointRouteBuilder MapExhibitionEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/exhibitions");

            group.MapGet("", GetExhibitions);
            group.MapGet("{id:int}", GetExhibitionById);
            group.MapPost("", AddExhibition);
            group.MapPut("{id:int}", UpdateExhibition);
            group.MapDelete("{id:int}", DeleteExhibition);

            return app;
        }

        private static async Task<Ok<IEnumerable<Exhibition>>> GetExhibitions(IExhibitionService service)
        {
            var list = await service.GetExhibitionsAsync();
            return TypedResults.Ok(list);
        }

        private static async Task<Results<NotFound, Ok<Exhibition>>> GetExhibitionById(int id, IExhibitionService service)
        {
            var exhibition = await service.GetExhibitionByIdAsync(id);
            return exhibition == null ? TypedResults.NotFound() : TypedResults.Ok(exhibition);
        }

        private static async Task<Results<Created<Exhibition>, BadRequest>> AddExhibition(Exhibition exhibition, IExhibitionService service)
        {
            var newExhibition = await service.CreateExhibitionAsync(exhibition);
            if (newExhibition is null) return TypedResults.BadRequest();

            return TypedResults.Created($"/api/exhibitions/{newExhibition.Id}", newExhibition);
        }

        private static async Task<Results<NoContent, NotFound>> UpdateExhibition(int id, Exhibition exhibition, IExhibitionService service)
        {
            exhibition.Id = id;
            if (await service.UpdateExhibitionAsync(exhibition))
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }

        private static async Task<Results<NoContent, NotFound>> DeleteExhibition(int id, IExhibitionService service)
        {
            if (await service.DeleteExhibitionAsync(id))
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }
    }
}
