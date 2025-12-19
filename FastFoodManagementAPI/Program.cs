using FastFoodManagementAPI.Features.Orders;
using FastFoodManagementAPI.Features.Products;
using FastFoodManagementAPI.Shared;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Register DbContext
// ----------------------
builder.Services.AddDbContext<FastFoodDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ----------------------
// Register Services
// ----------------------
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderService>();

// ----------------------
// Add Controllers (API)
// ----------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

// ----------------------
// Enable CORS for React Dev
// ----------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") // your React dev URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ----------------------
// Swagger / OpenAPI (optional)
// ----------------------
builder.Services.AddOpenApi();

var app = builder.Build();

// ----------------------
// Middleware
// ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Serve static files from wwwroot
app.UseStaticFiles();

app.UseRouting();

app.UseCors(); // enable CORS

app.UseAuthorization();

// ----------------------
// Map API Controllers
// ----------------------
app.MapControllers();

app.Run();
