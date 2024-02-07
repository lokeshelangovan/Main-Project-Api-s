using ApiForMenswear.Data;
using ApiForMenswear.Interface;
using ApiForMenswear.Repository;

using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MenswearContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MenswearContext"))
.LogTo(Console.WriteLine, LogLevel.Information));
builder.Services.AddScoped<IUserSignupRepository, UserSignupRepository>();
builder.Services.AddScoped<ISellerSignupRepository, SellerSignupRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductManagementRepository, ProductManagementRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();



var MyAllowSpecificationOrigin = "_MyAllowSpecificationOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificationOrigin,
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificationOrigin);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

 