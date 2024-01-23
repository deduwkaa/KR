using Microsoft.EntityFrameworkCore;
using OOPShop.Repositories.Interfaces;
using OOPShop.Repositories;
using OOPShop.Services.Interfaces;
using OOPShop.Services;
using OOPShop.Models;
using OOPShop.Data;

var builder = WebApplication.CreateBuilder(args);

// adding services
builder.Services.AddMvc();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IProductService, ProductService>();

builder.Services.AddSingleton<AbstractApplicationDbContext, ApplicationDbContext>();
builder.Services.AddSingleton<AuthUser, AuthUser>();
// adding repositories
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IOrderItemRepository, OrderItemRepository>();


var app = builder.Build();

// mapping controllers with routes
app.MapControllers();

app.Run();
