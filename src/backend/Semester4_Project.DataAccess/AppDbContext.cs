//connection to the database, every DbSet is a table

using Microsoft.EntityFrameworkCore;
using Semester4_Project.DataAccess.Entities;

namespace Semester4_Project.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //email has to be unique
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(256);
        modelBuilder.Entity<User>().Property(u => u.Role).HasMaxLength(20);

        //one profile per student, deleted with the account
        modelBuilder.Entity<StudentProfile>().HasIndex(p => p.UserId).IsUnique();
        modelBuilder.Entity<StudentProfile>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<StudentProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<StudentProfile>().Property(p => p.FullName).HasMaxLength(100);
    }
}
