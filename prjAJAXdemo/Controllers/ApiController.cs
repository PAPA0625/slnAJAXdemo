using Microsoft.AspNetCore.Mvc;

namespace prjAJAXdemo.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "小趴";
            }
            return Content($"<h2>{name}</h2>", "text/html", System.Text.Encoding.UTF8);

        }
    }
}
