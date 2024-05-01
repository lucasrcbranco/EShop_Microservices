﻿namespace Ordering.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public sealed record CreateOrderResult(Guid OrderId);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}