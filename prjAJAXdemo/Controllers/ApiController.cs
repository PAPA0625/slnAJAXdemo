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
            //System.Threading.Thread.Sleep(5000);
            if (string.IsNullOrEmpty(name))
            {
                name = "小趴";
            }
            return Content($"<h2>{name}</h2>", "text/html", System.Text.Encoding.UTF8);

        }
        public IActionResult register(Members member, IFormFile formFile)
        {

            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.name}，{member.email},  You are {member.age} years old.");
            string strpath = Path.Combine(_host.WebRootPath, "upload", formFile.FileName);

            using (var data = new FileStream(strpath, FileMode.Create))
            {
                formFile.CopyTo(data);
            }
            

            member.FileName = formFile.FileName;
            byte[] imgbyte = null;
            using(MemoryStream ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                imgbyte = ms.ToArray();
            }
            member.FileData = imgbyte;

            _context.Members.Add(member);
            _context.SaveChanges();

            return Content("新增成功");
        }
        public IActionResult CheckAccount(MemberViewModel vm)
        {
            if(_context.Members.Any(p => p.Name == vm.name))
            {
                return Content("此名稱已被使用");
            }
            else
            {
                return Content("此名稱可使用");
            }
        }
        public IActionResult city()
        {
            var cities = _context.Address.Select(c =>  c.City).Distinct();
            return Json(cities);
        }
        public IActionResult cube(string city)
        {
            var road = _context.Address.Where(c => c.City == city).Select(c => c.SiteId).Distinct();
            return Json(road);
        }
        public IActionResult road(string cube) 
        {
            var road = _context.Address.Where(c => c.SiteId == cube).Select(c => c.Road).Distinct();
            return Json(road);
        }
        public IActionResult getimgbyid(int id = 6)
        {
            Members members = _context.Members.Find(id);
            byte[] img = members.FileData;

            if(img != null)
            {
                return File(img, "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
