using Basket;
using Catalog;
using Ordering;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);


//configure the HTTP request pipeline.

var app = builder.Build();

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();


await app.RunAsync();
