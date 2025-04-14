using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TodoList.Services
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _context;

        public TodoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<(List<TodoItem> items, int totalCount)> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.TodoItems.AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(t => t.DueDate ?? DateTime.MaxValue)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.TodoItems.CountAsync();
        }

        public async Task AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(List<TodoItem> items, int totalCount)> SearchAsync(
         string searchTerm,
         bool? isCompleted,
         string priority,
         int pageNumber = 1,
         int pageSize = 10)
        {
            IQueryable<TodoItem> query = _context.TodoItems;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.Title.Contains(searchTerm) ||
                    (t.Description != null && t.Description.Contains(searchTerm)));
            }

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(t => t.Priority == priority);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(t => t.DueDate ?? DateTime.MaxValue)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, totalCount);
        }
    }
}