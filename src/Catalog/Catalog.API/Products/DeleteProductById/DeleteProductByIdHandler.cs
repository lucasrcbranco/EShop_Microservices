
namespace Catalog.API.Products.DeleteProductById;

public sealed record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdResult>;
public sealed record DeleteProductByIdResult(bool IsSuccess);

internal class DeleteProductByIdCommandHandler(IDocumentSession session, ILogger<DeleteProductByIdCommandHandler> logger)
    : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
{
    public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductByIdHandler.Handle called with {@Command}", command);

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductByIdResult(true);
    }
}
