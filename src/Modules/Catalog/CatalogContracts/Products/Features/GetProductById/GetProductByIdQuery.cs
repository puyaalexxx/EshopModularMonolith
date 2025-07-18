

namespace Catalog.Contracts.Products.Features.GetProductById
{
    public record class GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
}
