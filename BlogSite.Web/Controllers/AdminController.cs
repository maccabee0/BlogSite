using System.Linq;
using System.Web;
using BlogSite.Services.Abstract;
using BlogSite.Domain.Entities;
using System.Web.Mvc;
using BlogSite.Web.Models;

namespace BlogSite.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IService _service;
        //
        // GET: /Admin/
        public AdminController(IService repo)
        {
            _service = repo;
        }

        public ViewResult Index()
        {
            BlogListViewModel model = new BlogListViewModel()
                {
                    Blogs = _service.Blogs
                };
            return View();
        }

        public ViewResult Edit(int id)
        {
            Blog blog = _service.GetBlog(id);
            return View(blog);
        }

        [HttpPost]
        public ActionResult Edit(Blog blog, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                _service.SaveBlog(blog);
                TempData["message"] = string.Format("{0} has been saved", blog.Subject);
                return RedirectToAction("Index");
            }
            return View(blog);
        }
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Blog blog = _service.GetBlog(id);
            if (blog != null)
            {
                _service.DeleteBlog(blog);
                TempData["message"] = string.Format("The {0} blog by {1} was deleted", blog.Subject,blog.User.UserName);
            }
            return RedirectToAction("Index");
        }

        public ViewResult UserIndex()
        {
            return View(_service.Users);
        }

        [HttpPost]
        public ActionResult AdjustAdminStatus(int userId)
        {
            User user = _service.UserRepository.GetUser(userId);
            if (user != null)
            {
                _service.ChangeAdminStatus(user);
                TempData["message"] = string.Format("{0}'s Admin Status Changed ", user.UserName);
            }
            return RedirectToAction("UserIndex");
        }
    }
}
