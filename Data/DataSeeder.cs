using TodoList.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Data
{
    public static class DataSeeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Apply pending migrations
            context.Database.Migrate();

            if (!context.TodoItems.Any())
            {
                var priorities = new[] { "High", "Medium", "Low", null };
                var random = new Random();

                // Batch insert for better performance
                for (int i = 0; i < 100; i++) // 100 batches of 1000 = 100,000 records
                {
                    var batch = new List<TodoItem>();

                    for (int j = 0; j < 1000; j++)
                    {
                        var itemNumber = i * 1000 + j;
                        batch.Add(new TodoItem
                        {
                            Title = $"Task {itemNumber + 1}",
                            Description = $"Description for task {itemNumber + 1}",
                            DueDate = DateTime.UtcNow.AddDays(random.Next(-30, 30)),
                            Priority = priorities[random.Next(0, 4)],
                            IsCompleted = random.Next(0, 2) == 1
                        });
                    }

                    context.TodoItems.AddRange(batch);
                    context.SaveChanges();
                }
            }
        }
    }
}