using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Todo");
        }
    }
}