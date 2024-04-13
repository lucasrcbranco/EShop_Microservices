using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories) : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product(
            command.Name,
            command.Description,
            command.ImageFile,
            command.Price,
            command.Categories);

        return new CreateProductResult(Guid.NewGuid());
    }
}
