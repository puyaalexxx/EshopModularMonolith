
using Catalog.Contracts.Products.Features.GetProductById;

namespace Catalog.Products.Features.GetProductById
{
    public class GetProductsByCategoryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            //get products
            var product = await dbContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(query.Id);
            }

            //mapping product entity to productDto
            var productDto = product.Adapt<ProductDto>();

            //return result
            return new GetProductByIdResult(productDto);
        }
    }
}
