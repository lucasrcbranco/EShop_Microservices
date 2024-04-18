namespace Basket.API.Basket.DeleteBasket;

public sealed record DeleteBasketResponse(bool IsSuccess);


public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(userName));
            return Results.Ok(result.Adapt<DeleteBasketResponse>());
        })
        .Produces<DeleteBasketResponse>(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("GetBasketByUserName")
        .WithSummary("Get basket by username")
        .WithDescription("Get basket by username");
    }
}
