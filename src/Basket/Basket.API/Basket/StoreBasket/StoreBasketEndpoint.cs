namespace Basket.API.Basket.StoreBasket;

public sealed record StoreBasketRequest(ShoppingCart Cart);
public sealed record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest command, ISender sender) =>
        {
            var result = await sender.Send(command.Adapt<StoreBasketCommand>());
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{response.UserName}", response);
        })
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("UpsertBasket")
        .WithSummary("Inserts a new basket or Updates an existing one")
        .WithDescription("Inserts a new basket or Updates an existing one");
    }
}