using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: Todo
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _todoService.GetAllAsync();
            return View(items);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                await _todoService.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _todoService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Todo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TodoItem item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _todoService.UpdateAsync(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _todoService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _todoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Todo/ToggleComplete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var item = await _todoService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.IsCompleted = !item.IsCompleted;
            await _todoService.UpdateAsync(item);

            return RedirectToAction(nameof(Index));
        }

        // GET: Todo/Search
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm, bool? isCompleted, string priority)
        {
            var items = await _todoService.SearchAsync(searchTerm, isCompleted, priority);
            return View("Index", items);
        }
    }
}