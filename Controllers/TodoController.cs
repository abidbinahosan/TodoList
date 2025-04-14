using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;
using System.Threading.Tasks;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // ... (keep other existing actions unchanged) ...

        [HttpGet]  // Explicitly mark as GET
        public async Task<IActionResult> Search(string searchTerm, bool? isCompleted, string? priority)
        {
            // Normalize priority input
            priority = string.IsNullOrWhiteSpace(priority) ? null : priority.Trim();

            var items = await _todoService.SearchAsync(searchTerm, isCompleted, priority);

            // Pass filter values back to view
            ViewBag.SearchTerm = searchTerm;
            ViewBag.IsCompleted = isCompleted;
            ViewBag.Priority = priority;

            return View("Index", items);
        }
    }
}