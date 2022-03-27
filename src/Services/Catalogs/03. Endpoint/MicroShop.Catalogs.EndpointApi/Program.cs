using MicroShop.Catalogs.Core.Contracts.Products;
using MicroShop.Catalogs.Data.MongoCommand.Common;
using MicroShop.Catalogs.Data.MongoCommand.Products;
using MicroShop.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommon();

var connectionString = builder.Configuration.GetConnectionString("Catalog_ConnectionString");

builder.Services.AddMongodbContext<CatalogCommandDbContext>(options =>
{
    options.ConnectionString = connectionString;
});

builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
