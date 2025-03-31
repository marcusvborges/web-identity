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
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>()
            .HasMany(d => d.Sectors)     // Um Departamento tem muitos Setores
            .WithOne(s => s.Department)  // Cada Setor pertence a um Departamento
            .HasForeignKey(s => s.DepartmentId)  // Chave estrangeira no Setor
            .OnDelete(DeleteBehavior.Restrict);  // Impede deleção em cascata (opcional)

            // Relacionamento Setor (1) → (N) Employee
            modelBuilder.Entity<Sector>()
                .HasMany(s => s.Employees)    // Um Setor pode ter muitos Funcionários
                .WithOne(e => e.Sector)      // Cada Funcionário pertence a um Setor
                .HasForeignKey(e => e.SectorId)  // Chave estrangeira no Funcionário
                .OnDelete(DeleteBehavior.Restrict);  // Impede deleção em cascata (opcional)

            // Relacionamento Departamento (1) → (N) Employee
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)   // Um Departamento pode ter muitos Funcionários
                .WithOne(e => e.Department) // Cada Funcionário pertence a um Departamento
                .HasForeignKey(e => e.DepartmentId)  // Chave estrangeira no Funcionário
                .OnDelete(DeleteBehavior.Restrict);  // Impede deleção em cascata (opcional)

            modelBuilder.Entity<Employee>()
                .Property(f => f.HierarchicalLevel)
                .HasConversion<string>(); // Armazena como string no banco
        }
    }
}
