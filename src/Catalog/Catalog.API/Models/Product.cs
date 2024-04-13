namespace Catalog.API.Models;

public class Product
{
    public Product()
    {

    }

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

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; } = default!;

    public List<string> Categories { get; set; } = new();
}
