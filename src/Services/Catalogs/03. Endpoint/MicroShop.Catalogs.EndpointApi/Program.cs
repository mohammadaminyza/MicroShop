using MicroShop.Catalogs.Core.ApplicationServices.Products;
using MicroShop.Catalogs.Core.Contracts.Products;
using MicroShop.Catalogs.Data.MongoCommand.Common;
using MicroShop.Catalogs.Data.MongoCommand.Products;
using MicroShop.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommon();
builder.Services.AddMongodbContext<CatalogCommandDbContext>(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("Catalog_ConnectionString");
});

builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
builder.Services.AddScoped<AddProductServiceTest>();

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
