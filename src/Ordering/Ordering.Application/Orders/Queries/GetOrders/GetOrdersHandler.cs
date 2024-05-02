namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        List<Order> orders = await dbContext.Orders
            .AsNoTracking()
            .Include(o => o.OrderItems)
            .Skip(query.PaginationRequest.PageIndex * query.PaginationRequest.PageSize)
            .Take(query.PaginationRequest.PageSize)
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);

        long totalOrdersCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                query.PaginationRequest.PageIndex,
                query.PaginationRequest.PageSize,
                totalOrdersCount,
                orders.ToOrderDtoList()));
    }
}