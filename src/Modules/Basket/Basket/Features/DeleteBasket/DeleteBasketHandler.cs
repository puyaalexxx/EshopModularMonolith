namespace Basket.Features.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    internal class DeleteBasketHandler(/*IBasketRepository repository*/BasketDbContext basketDbContext)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //Delete Basket entity from command object
            //save to database
            //return result

            // await repository.DeleteBasket(command.UserName, cancellationToken);

            var basket = await basketDbContext.ShoppingCarts
                .FirstOrDefaultAsync(b => b.UserName == command.UserName, cancellationToken);

            if (basket == null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            basketDbContext.ShoppingCarts.Remove(basket);
            await basketDbContext.SaveChangesAsync(cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
