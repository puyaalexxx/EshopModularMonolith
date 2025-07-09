﻿

namespace Catalog.Products.Features.GetProductsByCategory
{
    //public record GetProductsByCategoryRequest(string Category);

    public record GetProductsByCategoryResponse(IEnumerable<ProductDto> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));

                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductsByCategory")
            .WithSummary("Get Products By Category")
            .WithDescription("Get products by category from the catalog.")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
