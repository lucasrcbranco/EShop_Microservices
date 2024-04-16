namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories) : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

        var product = new Product(
            command.Name,
            command.Description,
            command.ImageFile,
            command.Price,
            command.Categories);

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
