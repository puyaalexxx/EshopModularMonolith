﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Basket.Features.AddItemIntoBasket
{
    public record AddItemIntoBasketRequest(string UserName, ShoppingCartItemDto ShoppingCartItem);
    public record AddItemIntoBasketResponse(Guid ShoppingCartId);

    public class AddItemIntoBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/{userName}/items",
                async ([FromRoute] string userName, [FromBody] AddItemIntoBasketRequest request,
                       ISender sender) =>
                {
                    var command = new AddItemIntoBasketCommand(userName, request.ShoppingCartItem);

                    var result = await sender.Send(command);

                    var response = result.Adapt<AddItemIntoBasketResponse>();

                    return Results.Created($"/basket/{response.ShoppingCartId}", response);
                })
            .Produces<AddItemIntoBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add Item Into Basket")
            .WithDescription("Add Item Into Basket");
            // .RequireAuthorization();
        }
    }
}
