﻿namespace Basket.API.Basket.GetBasket;

public sealed record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public sealed record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketQueryHandler(IBasketRepository basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var result = await basketRepository.GetBasketByUserNameAsync(query.UserName, cancellationToken);
        return new GetBasketResult(result);
    }
}
