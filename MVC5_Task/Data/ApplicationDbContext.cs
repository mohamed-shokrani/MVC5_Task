using Microsoft.EntityFrameworkCore;
using MVC5_Task.Models;

namespace MVC5_Task.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                 base.OnModelCreating(modelBuilder);
                 modelBuilder.Entity<EmployeeProject>()
                .HasKey(e => new { e.EmployeeId, e.ProjectId });//Composite Key
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }    
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }


    }
}
