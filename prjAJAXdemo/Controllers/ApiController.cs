using Microsoft.AspNetCore.Mvc;
using prjAJAXdemo.Models;
using prjAJAXdemo.ViewModels;
using System.Diagnostics;

namespace prjAJAXdemo.Controllers
{
    public class ApiController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DemoContext _context;
        public ApiController(IWebHostEnvironment host, DemoContext context) 
        { 
            _host = host;
            _context = context;
        }
        public IActionResult Index(string name)
        {
            System.Threading.Thread.Sleep(5000);
            if (string.IsNullOrEmpty(name))
            {
                name = "小趴";
            }
            return Content($"<h2>{name}</h2>", "text/html", System.Text.Encoding.UTF8);

        }
        public IActionResult register(MemberViewModel member, IFormFile formFile)
        {

            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.name}，{member.email},  You are {member.age} years old.");
            string strpath = Path.Combine(_host.WebRootPath, "upload", formFile.FileName);

            using (var data = new FileStream(strpath, FileMode.Create))
            {
                formFile.CopyTo(data);
            }
            return Content(strpath);
        }
        public IActionResult CheckAccount(string name)
        {
            if(_context.Members.Any(Members => Members.Name == name))
            {
                return Content("此名稱已被使用");
            }
            else
            {
                return Content("此名稱可使用");
            }
        }
    }
}
