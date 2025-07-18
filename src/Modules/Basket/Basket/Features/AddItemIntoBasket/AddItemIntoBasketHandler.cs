using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Features.AddItemIntoBasket
{
    public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem)
    : ICommand<AddItemIntoBasketResult>;

    public record AddItemIntoBasketResult(Guid ShoppingCartId);

    internal class AddItemIntoBasketHandler(IBasketRepository repository /*BasketDbContext basketDbContext*/, ISender sender)
        : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
            // Add shopping cart item into shopping cart
            var shoppingCart = await repository.GetBasket(command.UserName, false, cancellationToken);

            var result = await sender.Send(new GetProductByIdQuery(command.ShoppingCartItem.ProductId), cancellationToken);

            shoppingCart.AddItem(
                   command.ShoppingCartItem.ProductId,
                   command.ShoppingCartItem.Quantity,
                   command.ShoppingCartItem.Color,
                  /* command.ShoppingCartItem.Price,
                   command.ShoppingCartItem.ProductName*/
                  result.Product.Price,
                  result.Product.Name);

            await repository.SaveChangesAsync(command.UserName, cancellationToken);

            /*var shoppingCart = await basketDbContext.ShoppingCarts
                .Include(sc => sc.Items)
                .FirstOrDefaultAsync(sc => sc.UserName == command.UserName, cancellationToken);

            if (shoppingCart == null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            shoppingCart.AddItem(
                    command.ShoppingCartItem.ProductId,
                    command.ShoppingCartItem.Quantity,
                    command.ShoppingCartItem.Color,
                    command.ShoppingCartItem.Price,
                    command.ShoppingCartItem.ProductName);

            await basketDbContext.SaveChangesAsync(cancellationToken); */

            return new AddItemIntoBasketResult(shoppingCart.Id);
        }
    }
}
