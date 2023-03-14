using BlogTutorial.Data;
using BlogTutorial.Models;
using BlogTutorial.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BlogTutorial.Controllers
{
    public class AdminController : Controller
    {
        AppDbContext db;
        IWebHostEnvironment env;
        public AdminController(AppDbContext _db, IWebHostEnvironment environment)
        {
            db = _db;
            env = environment; 
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddPost()
        {

            return View();
        }

        //Insert post data in Post Table database
        [HttpPost]
        public IActionResult AddPost(PostViewModel myPost)
        {
            if(ModelState.IsValid)
            {
                string ImageName = myPost.Image.FileName.ToString();
                var FolderPath =Path.Combine(env.WebRootPath, "images");   
                var CompletePicPath = Path.Combine(FolderPath, ImageName);
                myPost.Image.CopyTo(new FileStream(CompletePicPath, FileMode.Create));

                Post post = new Post();
                post.Title = myPost.Title;
                post.SubTital=myPost.SubTital;
                post.Date=myPost.Date;
                post.Content = myPost.Content;
                post.Slug = myPost.Slug;
                post.Image = ImageName;

                db.tblPost.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }            
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

        public IActionResult CreateProfile()
        {
            return View();
        }

        //Create Profile, Insert data in profile table database
        [HttpPost]
        public IActionResult CreateProfile(ProfileViewModel profileVm)
        {
            if (ModelState.IsValid)
            {
                string ImageName = profileVm.Image.FileName.ToString();
                var FolderPath = Path.Combine(env.WebRootPath, "images");
                var CompletePicPath = Path.Combine(FolderPath, ImageName);
                profileVm.Image.CopyTo(new FileStream(CompletePicPath, FileMode.Create));

                Profile profile = new Profile();
                profile.Fname = profileVm.Fname;
                profile.Lname= profileVm.Lname;
                profile.Bio= profileVm.Bio;
                profile.Image = ImageName;
                profile.Username = profileVm.Username;
                profile.Password = profileVm.Password;

                db.TblProfaie.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }             
            return View();           
        }

        public IActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = db.TblProfaie.Where(opt => opt.Username.Equals(loginViewModel.Username) && opt.Password.Equals(loginViewModel.Password)).FirstOrDefault();
                if (result!=null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                ViewData["LoginFlag"] = "Invalid Username or Password!";
                return View();
            }
            return View();
        }
    }
}
