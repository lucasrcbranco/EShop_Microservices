namespace Ordering.Application.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
public sealed record GetOrdersResult(PaginatedResult<OrderDto> Orders);