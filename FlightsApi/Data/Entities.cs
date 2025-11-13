using FlightsApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightsApi.Data
{
    public class Entities(DbContextOptions<Entities> options) : DbContext(options)
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flight> Flights => Set<Flight>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure, dp =>
            {
                dp.Property(p => p.Place).HasColumnName("DeparturePlace");
                dp.Property(p => p.Time).HasColumnName("DepartureTime");
            });

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival, ap =>
            {
                ap.Property(p => p.Place).HasColumnName("ArrivalPlace");
                ap.Property(p => p.Time).HasColumnName("ArrivalTime");
            });

            // To avoid race condition whe the same seat is booked simultaneously
            modelBuilder.Entity<Flight>().Property(p => p.RemainingNumberOfSeats).IsConcurrencyToken();
        }
    }
}