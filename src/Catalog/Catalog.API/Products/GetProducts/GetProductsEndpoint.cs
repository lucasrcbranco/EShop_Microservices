namespace Catalog.API.Products.GetProducts;

public sealed record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("GetProducts")
        .WithDescription("Get all products")
        .WithSummary("Get all products");
    }
}
