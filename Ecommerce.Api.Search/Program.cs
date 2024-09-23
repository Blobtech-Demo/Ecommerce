using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add Configuration Json file
builder.Configuration.AddJsonFile("appsettings.json",
    optional: true, reloadOnChange: true).AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddHttpClient("OrderService",config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
});
builder.Services.AddHttpClient("ProductService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
}).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5,_ => TimeSpan.FromMilliseconds(500)));
builder.Services.AddHttpClient("CustomerService", config => config.BaseAddress = new Uri(builder.Configuration["Services:Customers"]))
.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(2, _ => TimeSpan.FromMilliseconds(500)));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
