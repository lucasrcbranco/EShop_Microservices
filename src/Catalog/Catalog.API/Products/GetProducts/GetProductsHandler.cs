﻿namespace Catalog.API.Products.GetProducts;

public sealed record GetProductsQuery(int PageNumber, int PageSize) : IQuery<GetProductsResult>;
public sealed record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        return new GetProductsResult(products);
    }
}
