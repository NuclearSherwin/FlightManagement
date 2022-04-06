using FlightManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public  DbSet<Plane> Planes { get; set; }
    }
}