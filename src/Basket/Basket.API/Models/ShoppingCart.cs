﻿namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal CartTotal => Items.Sum(i => i.Total);

    public ShoppingCart()
    {

    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}
