namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 5;
    public string Value { get; set; }

    private OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (DefaultLength > value.Length)
        {
            throw new DomainException($"OrderName need to have at minimum {DefaultLength} characters");
        }

        return new OrderName(value);
    }
}
