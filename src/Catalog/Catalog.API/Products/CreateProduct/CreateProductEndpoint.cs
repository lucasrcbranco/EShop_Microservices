namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories);

public sealed record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Adapt<CreateProductCommand>());
            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("CreateProduct")
        .WithDescription("Creates a new Product")
        .WithSummary("Creates a new Product");
    }
}
