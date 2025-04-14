using Microsoft.AspNetCore.Mvc;
using TodoList.Services;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoService _todoService;

        public HomeController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Add these two actions for creating todos
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.TodoItem item)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddAsync(item);
                return RedirectToAction("Index", "Todo");
            }
            return View(item);
        }
    }
}