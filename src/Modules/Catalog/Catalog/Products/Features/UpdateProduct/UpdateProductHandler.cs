

namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            //update product entity from command object
            var product = await dbContext.Products.FindAsync(command.Product.Id, cancellationToken);

            if (product is null)
            {
                throw new Exception($"Product with ID {command.Product.Id} not found.");
            }

            UpdateProductWithNewValues(product, command.Product);

            //save to database
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new UpdateProductResult(true);
        }

        private void UpdateProductWithNewValues(Product product, ProductDto productDto)
        {
            product.Update(
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price);

        }
    }
}
