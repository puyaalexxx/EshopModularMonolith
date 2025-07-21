using Basket.Basket.Dtos;
using Basket.Basket.Models;
using Basket.Data.Repository;

namespace Basket.Basket.Features.CreateBasket
{
    public record CreateBasketCommand(ShoppingCartDto ShoppingCart) : ICommand<CreateBasketResult>;

    public record CreateBasketResult(Guid ShoppingCartId);

    public class CreateBasketHandler(/*BasketDbContext basketDbContext*/ IBasketRepository repository)
    : ICommandHandler<CreateBasketCommand, CreateBasketResult>
    {
        public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            //create Basket entity from command object
            //save to database
            //return result

            var shoppingCart = CreateNewBasket(command.ShoppingCart);

            /*basketDbContext.ShoppingCarts.Add(shoppingCart);
            await basketDbContext.SaveChangesAsync(cancellationToken);*/

            await repository.CreateBasket(shoppingCart, cancellationToken);

            return new CreateBasketResult(shoppingCart.Id);
        }

        private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto)
        {
            // create new basket
            var newBasket = ShoppingCart.Create(shoppingCartDto.UserName);

            shoppingCartDto.Items.ForEach(item =>
            {
                newBasket.AddItem(
                    item.ProductId,
                    item.Quantity,
                    item.Color,
                    item.Price,
                    item.ProductName);
            });

            return newBasket;
        }
    }
}
