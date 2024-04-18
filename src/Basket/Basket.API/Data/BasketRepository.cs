
using Basket.API.Exceptions;

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var shoppingCart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        if (shoppingCart is null)
        {
            throw new BasketNotFoundException(userName);
        }

        return shoppingCart;
    }


    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        session.Store(shoppingCart);
        await session.SaveChangesAsync(cancellationToken);

        return shoppingCart;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);

        return true;
    }
}
