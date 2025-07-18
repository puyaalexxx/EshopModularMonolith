using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts
{
    public record class GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;

    public record GetProductsResult(PaginatedResult<ProductDto> Products);
    public class GetProductsByCategoryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);

            //get products
            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            //mapping product entity to productDto
            var productDtos = products.Adapt<List<ProductDto>>();

            //return result
            return new GetProductsResult(
                new PaginatedResult<ProductDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    productDtos
                )
            );
        }
    }
}
