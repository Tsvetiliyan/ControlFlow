using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ControlFlow.Core.Entities.Tasks;
using ControlFlow.Core.Entities;

namespace ControlFlow.Data;

public class IdentityAppContext : IdentityDbContext<ApplicationUser>
{
    public IdentityAppContext(DbContextOptions<IdentityAppContext> options) : base(options)
    {
    }

    public DbSet<UserTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserTask>()
            .Property(t => t.Description)
            .HasColumnType("TEXT");
        modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.UserName).IsUnique();
        modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.PhoneNumber).IsUnique();
    }
}
