using ProductCategoryApi.Services;
using ProductCategoryApi.Filters;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingFilter>(); 
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<GlobalExceptionFilter>(); 
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>(); 
});

// Add services to the container.
builder.Services.AddControllers();

// Register ProductService as a singleton
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();