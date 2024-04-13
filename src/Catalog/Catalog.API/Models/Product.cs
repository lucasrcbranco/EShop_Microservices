namespace Catalog.API.Models;

public class Product
{
    public Product(
        string name,
        string description,
        string imageFile,
        decimal price,
        List<string> categories)
    {
        Name = name;
        Description = description;
        ImageFile = imageFile;
        Price = price;
        Categories = categories;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string ImageFile { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public List<string> Categories { get; private set; } = new();
}
