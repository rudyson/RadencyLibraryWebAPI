using Microsoft.EntityFrameworkCore;
using RadencyLibraryWebAPI.Controllers;
using RadencyLibraryWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<LibraryDbContext>();
builder.Services.AddTransient<BooksController>();
builder.Services.AddTransient<RecommendedController>();

/*
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
string connectionString = configuration.GetValue<string>("Data:FoodDeliveryShopProducts:ConnectionStrings");
builder.Services.AddDbContext<LibraryDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});
*/

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
app.MapControllerRoute(
	name: "api",
	pattern: "{controller}/{action}/{id?}");
SeedData.EnsurePopulated(app);
app.Run();
