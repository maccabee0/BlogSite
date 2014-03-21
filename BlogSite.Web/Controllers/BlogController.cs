using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using BlogSite.Web.Models;
using BlogSite.Domain.Entities;
using BlogSite.Services.Abstract;

namespace BlogSite.Web.Controllers
{
    public class BlogController : Controller
    {
        private IService _service;
        public int PageSize = 6;

        public BlogController(IService service)
        {
            _service = service;
        }

        [System.Web.Mvc.HttpGet]
        public ViewResult AllBlogs()
        {
            return View(new BlogListViewModel()
            {
                Blogs = _service.Blogs,
                PagingInfo = new PagingInfo
            {
                CurrentPage = 1,
                BlogsPerPage = PageSize,
                TotalBlogs = _service.Blogs.Count()
            }
            });
        }

        [System.Web.Http.HttpGet]
        public ViewResult GetBlogById(int id)
        {
            var blog = _service.GetBlog(id);
            if (blog != null)
            {
                return View("BlogPage", blog);
            }
            return View("AllBlogs");
        }

        public PartialViewResult BlogsPager(string page)
        {
            int cPage = page == null ? 1 : int.Parse(page);
            return PartialView(
                 _service.Blogs
                .OrderByDescending(b => b.PostDate)
                .Skip((cPage - 1) * PageSize)
                .Take(PageSize)
            );
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult PostComment(string text, int blogId)
        {
            if (_service.GetBlog(blogId) != null)
            {
                var comment = new Comment()
                    {
                        UserId = _service.GetUser(User.Identity.Name).UserId,
                        BlogId = blogId,
                        Text = text,
                        PostDate = DateTime.Now
                    };
                _service.SaveComment(comment);
            }
            var com = _service.GetCommentsByBlog(blogId).ToList();
            return PartialView("CommentSummary", com);
        }

        [System.Web.Mvc.Authorize]
        public ViewResult CreateNewBlog()
        {
            return View("Edit", new Blog());
        }

        [System.Web.Mvc.Authorize]
        public ViewResult Edit(int blogId)
        {
            return View(_service.GetBlog(blogId));
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.BlogId == 0)
                {
                    blog.UserId = _service.GetUser(User.Identity.Name).UserId;
                }
                _service.SaveBlog(blog);
                TempData["message"] = string.Format(" The {0} blog has been saved", blog.Subject);
                return RedirectToAction("AllBlogs");
            }
            return View(blog);
        }

        public FileUploadJsonResult GetText(HttpPostedFileBase textFile)
        {
            //string text;
            //HttpPostedFileBase f = Request.Files[0];
            //using (StreamReader sr = new StreamReader(f.InputStream))
            //{
            //    text = sr.ReadToEnd();
            //}
            ////var text = new byte[textFile.ContentLength];
            //var filetype = textFile.ContentType;
            //textFile.InputStream.Read(text, 0, textFile.ContentLength);
            return new FileUploadJsonResult
                {
                    Data = new
                        {
                            message = string.Format("{0} upload successful", 
                            Path.GetFileName(textFile.FileName))
                        }
                };
        }
    }
}
