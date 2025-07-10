﻿using Microsoft.Extensions.Logging;

namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductHandler(CatalogDbContext dbContext, ILogger<CreateProductHandler> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create product entity from command object
            var product = CreateNewProduct(command.Product);

            //save to database
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateProductResult(product.Id);
        }

        private Product CreateNewProduct(ProductDto productDto)
        {
            var product = Product.Create(
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price);

            return product;
        }
    }
}
