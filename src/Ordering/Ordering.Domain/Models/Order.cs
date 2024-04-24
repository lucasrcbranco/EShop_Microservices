namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;

    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => _orderItems.Sum(i => i.Price * i.Quantity);
        private set { }
    }

    public void AddOrderItem(OrderItem item)
    {
        _orderItems.Add(item);
    }

    public void RemoveOrderItem(OrderItemId itemId)
    {
        var item = _orderItems.FirstOrDefault(i => i.Id == itemId);
        if (item is not null)
        {
            _orderItems.Remove(item);
        }
    }
}
