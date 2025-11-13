using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FlightsApi.Data;

public class EntitiesFactory : IDesignTimeDbContextFactory<Entities>
{
    public Entities CreateDbContext(string[] args)
    {
        // Build configuration manually
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // assumes appsettings.json is in root
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<Entities>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new Entities(optionsBuilder.Options);
    }
}
