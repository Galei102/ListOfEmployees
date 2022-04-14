using ListOfEmployees.Models;
using Microsoft.EntityFrameworkCore;

namespace ListOfEmployees.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }
    }
}
