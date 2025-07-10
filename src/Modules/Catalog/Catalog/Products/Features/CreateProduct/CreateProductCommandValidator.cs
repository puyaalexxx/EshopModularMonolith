


namespace Catalog.Products.Features.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Product category is required.");
            RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("Product image file is required.");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Product price must be greater than 0.");
        }
    }
}
