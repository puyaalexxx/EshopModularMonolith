using Basket;
using Carter;
using Catalog;
using Ordering;
using Shared.Exceptions.Handler;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);

//global exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//configure the HTTP request pipeline.

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options => { });

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.Run();
