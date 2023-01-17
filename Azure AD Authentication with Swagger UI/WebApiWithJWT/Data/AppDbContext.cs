using Microsoft.EntityFrameworkCore;
using WebAPIwithJWT.Models;

namespace WebAPIwithJWT.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=EmployeeDB;Trusted_Connection=true");
        }

        public DbSet<Department> departments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
