using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebIdentity.Entities;

namespace WebIdentity.Context
{
    public class MySQLDbContext : IdentityDbContext
    {
        public MySQLDbContext(DbContextOptions<MySQLDbContext> options) :
            base(options) 
        { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
            .HasData(
                new Employee
                {
                    EmployeeId = 1,
                    Name = "AdmimInitialUser",
                    Email = "marcusvbs2018@gmail.com",
                    Idade = 25
                }
            );
        }
    }
}
