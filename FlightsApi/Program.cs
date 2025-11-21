using FlightsApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // For OpenApiServer

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

// API service
builder.Services.AddDbContext<Entities>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.DescribeAllParametersInCamelCase(); // needed to make dotnet and angular query params compatible
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightsApi v1");
    });
}

// Optional command line params for seeding and seeding with reset
if (args.Contains("seed"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<Entities>();
    await db.Database.MigrateAsync(); // optional
    await DbSeeder.SeedAsync(db);
    return;
}

if (args.Contains("reset-seed"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<Entities>();
    await db.Database.MigrateAsync(); // optional
    await DbSeeder.ResetAndSeedAsync(db);
    return;
}

// We don't use authorisation yet
//app.UseAuthorization();

app.MapControllers();

// CORS
app.UseCors("AllowAngularApp");

app.Run();
