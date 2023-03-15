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
            if (HttpContext.Session.GetString("LoginFlage") != null)
            {
                ViewBag.NumberOfPost = db.tblPost.Count();
                ViewBag.NumberOfUser=db.TblProfaie.Count();
                CallSessionId();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult AddPost()
        {
            if (HttpContext.Session.GetString("LoginFlage") != null)
            {
                CallSessionId();
                return View();
            }
            else
            {
                //return RedirectToAction("Login", "Admin");
                return Redirect("/Admin/Login?ReturnUrl=/Admin/AddPost");
            }            
        }

        //Insert post data in Post Table database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(PostViewModel myPost)
        {
            if (HttpContext.Session.GetString("LoginFlage") != null)
            {
                CallSessionId();
                if (ModelState.IsValid)
                {
                    string ImageName = myPost.Image.FileName.ToString();
                    var FolderPath = Path.Combine(env.WebRootPath, "images");
                    var CompletePicPath = Path.Combine(FolderPath, ImageName);
                    myPost.Image.CopyTo(new FileStream(CompletePicPath, FileMode.Create));

                    Post post = new Post();
                    post.Title = myPost.Title;
                    post.SubTital = myPost.SubTital;
                    post.Date = myPost.Date;
                    post.Content = myPost.Content;
                    post.Slug = myPost.Slug;
                    post.Image = ImageName;

                    db.tblPost.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        //Display data on EditPost page
        public IActionResult EditPost()
        {
            if (HttpContext.Session.GetString("LoginFlage") != null)
            {
                CallSessionId();
                var myEditPost = db.tblPost;
                return View(myEditPost);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        //Delete Post from Database on EditPost page
        public IActionResult DeletePost(int id)
        {            
                var DeletePost = db.tblPost.Find(id);
                if (DeletePost != null)
                {
                    db.tblPost.Remove(DeletePost);
                    db.SaveChanges();
                }
                return RedirectToAction("EditPost", "Admin");            
        }

        //Update Action and redirect on UpdatePage with selected data by Id
        public IActionResult UpdatePost(int id)
        {
            if (HttpContext.Session.GetString("LoginFlage") != null)
            {
                CallSessionId();
                var postupdate = db.tblPost.Find(id);
                return View(postupdate);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Update data in database on update page
        public IActionResult UpdatePost(Post post)
        {
            db.tblPost.Update(post);
            db.SaveChanges();
            return RedirectToAction("EditPost", "Admin");
        }

        public IActionResult CreateProfile()
        {
            if (HttpContext.Session.GetString("LoginFlage") != null)
            {
                CallSessionId();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        //Create Profile, Insert data in profile table database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProfile(ProfileViewModel profileVm)
        {
            if (HttpContext.Session.GetString("LoginFlage") != null)
            {
                CallSessionId();
                if (ModelState.IsValid)
                {
                    string ImageName = profileVm.Image.FileName.ToString();
                    var FolderPath = Path.Combine(env.WebRootPath, "images");
                    var CompletePicPath = Path.Combine(FolderPath, ImageName);
                    profileVm.Image.CopyTo(new FileStream(CompletePicPath, FileMode.Create));

                    Profile profile = new Profile();
                    profile.Fname = profileVm.Fname;
                    profile.Lname = profileVm.Lname;
                    profile.Bio = profileVm.Bio;
                    profile.Image = ImageName;
                    profile.Username = profileVm.Username;
                    profile.Password = profileVm.Password;

                    db.TblProfaie.Add(profile);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        public IActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel, string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = db.TblProfaie.Where(opt => opt.Username.Equals(loginViewModel.Username) && opt.Password.Equals(loginViewModel.Password)).FirstOrDefault();
                if (result!=null)
                {
                    HttpContext.Session.SetInt32("ProfileId", result.Id);
                    HttpContext.Session.SetString("LoginFlage", "True");
                    if (ReturnUrl == null)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }

                }
                ViewData["LoginFlag"] = "Invalid Username or Password!";
                return View();
            }
            return View();
        }

        public void CallSessionId()
        {           
            ViewBag.Profile = db.TblProfaie.Where(x => x.Id.Equals(HttpContext.Session.GetInt32("ProfileId"))).FirstOrDefault();
        }

       public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Home");
        }
    }
}
