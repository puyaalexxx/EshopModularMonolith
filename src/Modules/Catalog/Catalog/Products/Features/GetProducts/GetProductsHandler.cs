
using Mapster;

namespace Catalog.Products.Features.GetProducts
{
    public record class GetProductsQuery : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<ProductDto> Products);

    public class GetProductsByCategoryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            //get products
            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            //mapping product entity to productDto
            var productDtos = products.Adapt<List<ProductDto>>();

            //return result
            return new GetProductsResult(productDtos);
        }
    }
}
