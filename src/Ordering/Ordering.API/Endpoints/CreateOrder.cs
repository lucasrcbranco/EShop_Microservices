using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

public sealed record CreateOrderRequest(OrderDto Order);
public sealed record CreateOrderResponse(Guid OrderId);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var result = await sender.Send(request.Adapt<CreateOrderCommand>());
            var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/orders/{response.OrderId}", response);
        })
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("CreateOrder")
        .WithDescription("Creates a new Order")
        .WithSummary("Creates a new Order");
    }
}
