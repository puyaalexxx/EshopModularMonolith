﻿


namespace Basket.Features.CreateBasket
{
    public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart.UserName)
                .NotEmpty()
                .WithMessage("UserName us required.");
        }
    }
}
