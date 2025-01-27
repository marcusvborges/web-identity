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

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    UserId = 1,
                    Name = "AdmimInitialUser",
                    Email = "marcusvbs2018@gmail.com",
                    Idade = 25
                }
            );
        }
    }
}
