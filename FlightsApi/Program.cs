using FlightsApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // For OpenApiServer
using FlightsApi.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

// API service
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightsApi", Version = "1.0" });
    c.EnableAnnotations(); // if using [SwaggerOperation]
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "http://localhost:5203"
    });
    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});

// CORS service
// currently http://localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
var random = new Random();

// Mock data: flights
Flight[] flightsToSeed = new Flight[]
{
    new (Guid.NewGuid(),
            "American Airlines",
            random.Next(90, 5000).ToString(),
            new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1, 3))),
            new TimePlace("Istanbul", DateTime.Now.AddHours(random.Next(4, 10))),
            random.Next(1, 853)),
    new (Guid.NewGuid(),
            "Deutsche BA",
            random.Next(90, 5000).ToString(),
            new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(1, 10))),
            new TimePlace("Schiphol", DateTime.Now.AddHours(random.Next(4, 15))),
            random.Next(1, 853)),
    new (Guid.NewGuid(),
            "British Airways",
            random.Next(90, 5000).ToString(),
            new TimePlace("London, England", DateTime.Now.AddHours(random.Next(1, 15))),
            new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4, 18))),
                random.Next(1, 853)),
    new (Guid.NewGuid(),
            "Basiq Air",
            random.Next(90, 5000).ToString(),
            new TimePlace("Amsterdam", DateTime.Now.AddHours(random.Next(1, 21))),
            new TimePlace("Glasgow, Scotland", DateTime.Now.AddHours(random.Next(4, 21))),
                random.Next(1, 853)),
    new (Guid.NewGuid(),
            "BB Heliag",
            random.Next(90, 5000).ToString(),
            new TimePlace("Zurich", DateTime.Now.AddHours(random.Next(1, 23))),
            new TimePlace("Baku", DateTime.Now.AddHours(random.Next(4, 25))),
                random.Next(1, 853)),
    new (Guid.NewGuid(),
            "Adria Airways",
            random.Next(90, 5000).ToString(),
            new TimePlace("Ljubljana", DateTime.Now.AddHours(random.Next(1, 15))),
            new TimePlace("Warsaw", DateTime.Now.AddHours(random.Next(4, 19))),
                random.Next(1, 853)),
    new (Guid.NewGuid(),
            "ABA Air",
            random.Next(90, 5000).ToString(),
            new TimePlace("Praha Ruzyne", DateTime.Now.AddHours(random.Next(1, 55))),
            new TimePlace("Paris", DateTime.Now.AddHours(random.Next(4, 58))),
                random.Next(1, 853)),
    new (Guid.NewGuid(),
            "AB Corporate Aviation",
            random.Next(90, 5000).ToString(),
            new TimePlace("Le Bourget", DateTime.Now.AddHours(random.Next(1, 58))),
            new TimePlace("Zagreb", DateTime.Now.AddHours(random.Next(4, 60))),
                random.Next(1, 853))
};
entities.Flights.AddRange(flightsToSeed);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightsApi v1");
    });
}

// We don't use authorisation yet
//app.UseAuthorization();

app.MapControllers();

// CORS
app.UseCors("AllowAngularApp");

app.Run();
