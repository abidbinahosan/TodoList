using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoList.Services
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _context;

        public TodoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TodoItem>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCountAsync()
        {
            throw new NotImplementedException();
        }

        // ... (keep other existing methods unchanged) ...

        public async Task<List<TodoItem>> SearchAsync(string searchTerm, bool? isCompleted, string priority)
        {
            IQueryable<TodoItem> query = _context.TodoItems.AsQueryable();

            // Apply search term filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.Title.Contains(searchTerm) ||
                    (t.Description != null && t.Description.Contains(searchTerm)));
            }

            // Apply completion filter
            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            // Apply priority filter (safe null handling)
            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(t => t.Priority == priority);
            }

            // Safe sorting with null handling
            return await query
                .OrderBy(t => t.DueDate ?? DateTime.MaxValue)
                .ThenBy(t => t.Priority ?? "ZZZ")  // Puts null priorities last
                .AsNoTracking()
                .ToListAsync();
        }

        public Task UpdateAsync(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}