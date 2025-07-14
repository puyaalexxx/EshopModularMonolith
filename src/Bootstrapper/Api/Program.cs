using Basket;
using Carter;
using Catalog;
using Ordering;
using Serilog;
using Shared.Exceptions.Handler;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

builder.Services.AddCarterWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddMediatRWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);

//global exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//configure the HTTP request pipeline.

var app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

await app.RunAsync();
