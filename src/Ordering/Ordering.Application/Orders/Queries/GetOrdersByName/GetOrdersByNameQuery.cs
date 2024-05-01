namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public sealed record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;
public sealed record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);