namespace Catalog.API.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories) : ICommand<UpdateProductResult>;

public sealed record UpdateProductResult(Product Product);

internal class UpdateProductCommandHandler(IDocumentSession session, Logger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if(product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Update(command.Name, command.Description, command.ImageFile, command.Price, command.Categories);
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product);
    }
}
