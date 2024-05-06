namespace Basket.API.Basket.CheckoutBasket;

public sealed record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
public sealed record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest command, ISender sender) =>
        {
            var result = await sender.Send(command.Adapt<CheckoutBasketCommand>());
            var response = result.Adapt<CheckoutBasketResponse>();
            return Results.Ok(response);
        })
        .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithName("CheckoutBasket")
        .WithSummary("Checkouts an existing basket")
        .WithDescription("Checkouts an existing basket");
    }
}