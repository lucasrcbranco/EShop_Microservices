namespace Catalog.API.Products.GetProductById;

public sealed record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("GetProductById")
        .WithDescription("Get a Product by Id")
        .WithSummary("Get a Product by Id");
    }
}
