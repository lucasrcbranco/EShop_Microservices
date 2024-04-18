namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            return Results.Ok(result.Adapt<GetBasketResponse>());
        })
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("GetBasketByUserName")
        .WithSummary("Get basket by username")
        .WithDescription("Get basket by username");
    }
}
