namespace Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketCommand(string UserName, Guid ProductId) : ICommand<RemoveItemFromBasketResult>;
    public record RemoveItemFromBasketResult(Guid ShoppingCartId);

    internal class RemoveItemFromBasketHandler(IBasketRepository repository /*BasketDbContext basketDbContext*/)
        : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);

            shoppingCart.RemoveItem(command.ProductId);

            await repository.SaveChangesAsync(command.UserName, cancellationToken);

            /*var shoppingCart = await basketDbContext.ShoppingCarts.
                Include(sc => sc.Items)
                .FirstOrDefaultAsync(sc => sc.UserName == command.UserName, cancellationToken);

            if (shoppingCart == null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            shoppingCart.RemoveItem(command.ProductId);

            await basketDbContext.SaveChangesAsync(cancellationToken);*/

            return new RemoveItemFromBasketResult(shoppingCart.Id);
        }
    }
}
