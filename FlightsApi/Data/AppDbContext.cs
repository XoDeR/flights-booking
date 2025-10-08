using Microsoft.EntityFrameworkCore;

namespace FlightsApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // usage example
    //public DbSet<Product> Products => Set<Product>();
}