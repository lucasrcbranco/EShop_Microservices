namespace Catalog.API.Products.GetProductsByCategory;

public sealed record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
public sealed record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Categories.Any(c => c.Equals(query.Category, StringComparison.InvariantCultureIgnoreCase)))
            .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult(products);
    }
}
