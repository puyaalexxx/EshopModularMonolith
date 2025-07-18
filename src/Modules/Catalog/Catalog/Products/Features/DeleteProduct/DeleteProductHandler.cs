namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            //delete product entity from command object
            var product = await dbContext.Products.FindAsync(command.ProductId, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(command.ProductId);
            }

            //save to database
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new DeleteProductResult(true);
        }
    }
}
