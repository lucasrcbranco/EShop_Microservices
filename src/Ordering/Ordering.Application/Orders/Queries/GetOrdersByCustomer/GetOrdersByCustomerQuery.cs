namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public sealed record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;
public sealed record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);