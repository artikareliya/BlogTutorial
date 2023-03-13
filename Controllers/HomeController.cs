using BlogTutorial.Data;
using BlogTutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogTutorial.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;
        public HomeController(AppDbContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            SharedLayoutData();
            IEnumerable<Post> myPost = db.tblPost;
            return View(myPost);
        }

        [Route("Home/Post/{Slug}")]
        public IActionResult Post(string slug)
        {
            SharedLayoutData();
            var readpost= db.tblPost.Where(x => x.Slug ==slug).FirstOrDefault();
            return View(readpost);
        }

        public void SharedLayoutData()
        {
            ViewBag.post =db.tblPost;
            ViewBag.profile = db.TblProfaie.FirstOrDefault();
           
        }
    }
}
