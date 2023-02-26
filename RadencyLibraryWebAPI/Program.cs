using RadencyLibraryWebAPI.Controllers;
using RadencyLibraryWebAPI.Models;
using RadencyLibraryWebAPI.Models.DTO;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Registration DbContext
builder.Services.AddDbContext<LibraryDbContext>();
//builder.Services.AddTransient<ILibraryRepository, LibraryEFRepository>();
builder.Services.AddTransient<BooksController>();
builder.Services.AddTransient<RecommendedController>();
// Json Options
builder.Services.AddControllers().AddJsonOptions(
	x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers().AddJsonOptions(
	x => x.JsonSerializerOptions.WriteIndented = true);
// Auto Mapper initialization
builder.Services.AddAutoMapper(
	typeof(Program).Assembly,
	typeof(BookCompactDto).Assembly,
	typeof(BookDetailedDto).Assembly,
	typeof(ReviewDetailedDto).Assembly,
	typeof(BookIdDto).Assembly,
	typeof(RatingIdDto).Assembly,
	typeof(RatingNewDto).Assembly,
	typeof(ReviewIdDto).Assembly,
	typeof(ReviewNewDto).Assembly,
	typeof(BookNewDto).Assembly
	);

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
// Generating seed data
SeedData.EnsurePopulated(app);
// Application startup
app.Run();