using Basket.Basket.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Basket.Basket.Features.CreateBasket
{
    public record CreateBasketRequest(ShoppingCartDto ShoppingCart);
    public record CreateBasketResponse(Guid ShoppingCartId);

    public class CreateBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket",
                async (CreateBasketRequest request, ISender sender/*, ClaimsPrincipal user*/) =>
                {
                    //  var userName = user.Identity!.Name;
                    //   var updatedShoppingCart = request.ShoppingCart with { UserName = userName };

                    //var command = new CreateBasketCommand(updatedShoppingCart);

                    var command = request.Adapt<CreateBasketCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateBasketResponse>();

                    return Results.Created($"/basket/{response.ShoppingCartId}", response);
                })
            .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Basket")
            .WithDescription("Create Basket Description");
            //.RequireAuthorization();
        }
    }
}
