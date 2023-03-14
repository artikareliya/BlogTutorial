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

        //Display data on EditPost page
        public IActionResult EditPost()
        {
            var myEditPost = db.tblPost;
            return View(myEditPost);
        }

        //Delete Post from Database on EditPost page
        public IActionResult DeletePost(int id)
        {
            var DeletePost = db.tblPost.Find(id);
            if (DeletePost!=null)
            {           
                db.tblPost.Remove(DeletePost);
                db.SaveChanges();
            }
            return RedirectToAction("EditPost", "Admin");
        }

        //Update Action and redirect on UpdatePage with selected data by Id
        public IActionResult UpdatePost(int id)
        {
            var postupdate = db.tblPost.Find(id);
           return View(postupdate);
        }

        [HttpPost]
        //Update data in database on update page
        public IActionResult UpdatePost(Post post)
        {
            db.tblPost.Update(post);
            db.SaveChanges();
            return RedirectToAction("EditPost", "Admin");
        }
    }
}
