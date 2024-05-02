using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public sealed record UpdateOrderRequest(OrderDto OrderDto);
public sealed record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Adapt<UpdateOrderCommand>());
            var response = result.Adapt<UpdateOrderResponse>();

            return Results.Ok(response);
        })
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithName("UpdateOrder")
        .WithDescription("Updates an existing Order")
        .WithSummary("Updates an existing Order");
    }
}
