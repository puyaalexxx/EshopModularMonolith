

using Catalog.Contracts.Products.Features.GetProductById;

namespace Catalog.Products.Features.GetProductById
{
    //public record GetProductByIdRequest();

    public record GetProductByIdResponse(ProductDto Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByIdProduct")
            .WithSummary("Get Product By Id")
            .WithDescription("Get product by id from the catalog.")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
