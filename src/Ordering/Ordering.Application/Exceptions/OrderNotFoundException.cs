using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

internal class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid Id) : base(nameof(Order), Id) { }
}
