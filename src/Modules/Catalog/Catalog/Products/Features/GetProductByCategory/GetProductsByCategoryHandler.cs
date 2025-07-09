namespace Catalog.Products.Features.GetProductsByCategory
{
    public record class GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

    public record GetProductsByCategoryResult(IEnumerable<ProductDto> Products);

    public class GetProductsByCategoryHandler(CatalogDbContext dbContext) :
        IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            //get products
            var products = await dbContext.Products
                .AsNoTracking()
                .Where(p => p.Category.Contains(query.Category))
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            //mapping product entity to productDto
            var productDtos = products.Adapt<List<ProductDto>>();

            //return result
            return new GetProductsByCategoryResult(productDtos);
        }
    }
}
