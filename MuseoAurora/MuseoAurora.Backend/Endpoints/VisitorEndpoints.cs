// Endpoints/VisitorEndpoints.cs
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Backend.Services.Interfaces;
using MuseoAurora.Models;

namespace MuseoAurora.Backend.Endpoints
{
    public static class VisitorEndpoints
    {
        public static IEndpointRouteBuilder MapVisitorEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/visitors");

            group.MapGet("", GetVisitors);
            group.MapGet("{id:int}", GetVisitorById);
            group.MapPost("", AddVisitor);
            group.MapPut("{id:int}", UpdateVisitor);
            group.MapDelete("{id:int}", DeleteVisitor);

            return app;
        }

        private static async Task<Ok<IEnumerable<Visitor>>> GetVisitors(IVisitorService service)
        {
            var list = await service.GetVisitorsAsync();
            return TypedResults.Ok(list);
        }

        private static async Task<Results<NotFound, Ok<Visitor>>> GetVisitorById(int id, IVisitorService service)
        {
            var visitor = await service.GetVisitorByIdAsync(id);
            return visitor == null ? TypedResults.NotFound() : TypedResults.Ok(visitor);
        }

        private static async Task<Results<Created<Visitor>, BadRequest>> AddVisitor(Visitor visitor, IVisitorService service)
        {
            var newVisitor = await service.CreateVisitorAsync(visitor);
            if (newVisitor is null) return TypedResults.BadRequest();

            return TypedResults.Created($"/api/visitors/{newVisitor.Id}", newVisitor);
        }

        private static async Task<Results<NoContent, NotFound>> UpdateVisitor(int id, Visitor visitor, IVisitorService service)
        {
            visitor.Id = id;
            if (await service.UpdateVisitorAsync(visitor))
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }

        private static async Task<Results<NoContent, NotFound>> DeleteVisitor(int id, IVisitorService service)
        {
            if (await service.DeleteVisitorAsync(id))
            {
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }
    }
}