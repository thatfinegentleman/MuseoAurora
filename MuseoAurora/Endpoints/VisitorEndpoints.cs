using Microsoft.AspNetCore.Http.HttpResults;
using MuseoAurora.Models;
using MuseoAurora.Services;

namespace MuseoAurora.Endpoints
{
    public static class VisitorEndpoints
    {
        public static IEndpointRouteBuilder MapVisitors(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/visitors/");
            group.MapGet("", GetVisitors);
            group.MapGet("{id:int}", GetVisitorById);
            group.MapPost("", CreateVisitor);
            group.MapPut("", UpdateVisitor);
            group.MapDelete("{id:int}", DeleteVisitorById);
            return app;
        }

        public static async Task<Ok<IEnumerable<Visitor>>> GetVisitors(IVisitorService service)
        {
            var items = await service.GetVisitorsAsync();
            return TypedResults.Ok(items);
        }

        public static async Task<Results<NotFound, Ok<Visitor>>> GetVisitorById(IVisitorService service, int id)
        {
            var item = await service.GetVisitorByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            return TypedResults.Ok(item);
        }

        public static async Task<Results<BadRequest<string>, Ok<Visitor>>> CreateVisitor(IVisitorService service, Visitor item)
        {
            if (item == null) return TypedResults.BadRequest("Invalid payload");
            var result = await service.CreateVisitorAsync(item);
            if (!result.IsSuccess) return TypedResults.BadRequest(result.ErrorMessage!);
            return TypedResults.Ok(result.Data!);
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> UpdateVisitor(IVisitorService service, Visitor item)
        {
            var existingItem = await service.GetVisitorByIdAsync(item.Id);
            if (existingItem == null) return TypedResults.NotFound();
            var success = await service.UpdateVisitorAsync(item);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, BadRequest, Ok>> DeleteVisitorById(IVisitorService service, int id)
        {
            var item = await service.GetVisitorByIdAsync(id);
            if (item == null) return TypedResults.NotFound();
            var success = await service.DeleteVisitorAsync(id);
            if (!success) return TypedResults.BadRequest();
            return TypedResults.Ok();
        }
    }
}