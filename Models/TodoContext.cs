using Microsoft.EntityFrameworkCore;

namespace CSandunBlogQueryFilterEfCoreApi.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Query Filter
        modelBuilder.Entity<TodoItem>().HasQueryFilter(o => !o.IsDelete);
        
        
        modelBuilder.Entity<TodoItem>()
            .Property(o => o.Name)
            .IsRequired();
        modelBuilder.Entity<TodoItem>().Property(o => o.IsDelete)
            .HasDefaultValue(0)
            .IsRequired();

        modelBuilder.Entity<TodoItem>()
            .HasData(new TodoItem()
                {
                    Id = 1,
                    Name = "Write blog post for query filter.",
                    IsComplete = true
                },
                new TodoItem()
                {
                    Id = 2,
                    Name = "Create personal web site.",
                    IsComplete = true
                },
                new TodoItem()
                {
                    Id = 3,
                    Name = "Deleted todo item",
                    IsComplete = true
                });
    }
}