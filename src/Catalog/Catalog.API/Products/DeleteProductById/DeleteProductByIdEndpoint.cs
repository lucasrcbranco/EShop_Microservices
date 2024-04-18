namespace Catalog.API.Products.DeleteProductById;

public sealed record DeleteProductByIdResponse(bool IsSuccess);

public class DeleteProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductByIdCommand(id));
            var response = result.Adapt<DeleteProductByIdResponse>();
            return Results.Ok(response);
        })
        .Produces<DeleteProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("DeleteProductById")
        .WithDescription("Deletes an Existing Product by Id")
        .WithSummary("Deletes an Existing Product by Id");
    }
}
