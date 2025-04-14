using TodoList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoList.Services
{
    public interface ITodoService
    {
        Task<TodoItem> GetByIdAsync(int id);
        Task<(List<TodoItem> items, int totalCount)> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<int> GetTotalCountAsync();
        Task AddAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(int id);
        Task<(List<TodoItem> items, int totalCount)> SearchAsync(string searchTerm, bool? isCompleted, string priority, int pageNumber = 1, int pageSize = 10);
    }
}