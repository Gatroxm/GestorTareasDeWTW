using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;

namespace TaskManager.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Índice para optimizar búsquedas por usuario y estado
        modelBuilder.Entity<TaskItem>()
            .HasIndex(t => new { t.UserId, t.Status })
            .HasDatabaseName("IX_Tasks_User_Status");
    }
}