
namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellationToken)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellationToken))
        {
            return;
        }

        session.Store<Product>(DefaultProducts);
        await session.SaveChangesAsync(cancellationToken);
    }

    private static IEnumerable<Product> DefaultProducts
    {
        get
        {
            var products = new List<Product>
        {
            new("IPhone X", "IPhone X Description", "iphone-x.png", 950.00M, ["Smartphone"]),
            new("IPhone XI", "IPhone XI Description", "iphone-xi.png", 1100.00M, ["Smartphone"]),
            new("Samsung 10", "Samsung 10 Description", "samsung-10.png", 1100.00M, ["Smartphone"])
        };

            return products;
        }
    }
}
