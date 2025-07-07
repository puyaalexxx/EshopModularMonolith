namespace Catalog.Products.Dtos
{
    public record class ProductDto(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
}
