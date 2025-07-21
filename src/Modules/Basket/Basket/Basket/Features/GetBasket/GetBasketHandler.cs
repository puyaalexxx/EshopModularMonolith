using Basket.Basket.Dtos;
using Basket.Data.Repository;

namespace Basket.Basket.Features.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCartDto ShoppingCart);

    internal class GetBasketHandler(IBasketRepository repository /*BasketDbContext basketDbContext*/) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            // get basket with userName
            var basket = await repository.GetBasket(query.UserName, true, cancellationToken);

            /*var basket = await basketDbContext.ShoppingCarts
                .AsNoTracking()
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.UserName == query.UserName, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(query.UserName);
            }*/

            //mapping basket entity to shoppingcartdto
            var basketDto = basket.Adapt<ShoppingCartDto>();

            return new GetBasketResult(basketDto);
        }
    }
}
