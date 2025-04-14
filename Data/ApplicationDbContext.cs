using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // For performance with large datasets
            modelBuilder.Entity<TodoItem>()
                .HasIndex(t => t.IsCompleted)
                .IncludeProperties(t => new { t.Title, t.DueDate, t.Priority });

            modelBuilder.Entity<TodoItem>()
                .HasIndex(t => t.Priority);
        }
    }
}