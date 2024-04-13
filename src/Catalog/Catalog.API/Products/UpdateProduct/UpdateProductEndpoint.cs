namespace Catalog.API.Products.UpdateProduct;

public sealed record UpdateProductRequest(
    Guid Id,
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories);

public sealed record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Adapt<UpdateProductCommand>());
            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithName("UpdateProduct")
        .WithDescription("Updates an Existing Product")
        .WithSummary("Updates an Existing Product");
    }
}
