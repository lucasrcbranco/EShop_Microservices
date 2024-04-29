namespace OrderIteming.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder
            .Property(oi => oi.Id)
            .HasConversion(OrderItemId => OrderItemId.Value, dbId => OrderItemId.Of(dbId));

        builder
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder
            .HasOne<Order>()
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.Price).IsRequired();
    }
}
