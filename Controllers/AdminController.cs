using BlogTutorial.Data;
using BlogTutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogTutorial.Controllers
{
    public class AdminController : Controller
    {
        AppDbContext db;
        public AdminController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddPost()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddPost(Post myPost)
        {
            db.tblPost.Add(myPost);
            db.SaveChanges();
            return View();
        }
    }
}
