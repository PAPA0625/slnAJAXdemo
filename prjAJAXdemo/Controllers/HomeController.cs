using Microsoft.AspNetCore.Mvc;
using prjAJAXdemo.Models;
using System.Diagnostics;

namespace prjAJAXdemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _context;

        public HomeController(ILogger<HomeController> logger, DemoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult First()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Member()
        {
            return View(_context.Members);
        }
        public IActionResult Address()
        {
            return View();
        }
        public IActionResult fetch()
        {
            return View();
        }
    }
}