using Microsoft.AspNetCore.Mvc;
using BlogEngine.Core;

namespace BlogEngine.NET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // This replaces the functionality of default.aspx
            return View();
        }

        public IActionResult Post(string id)
        {
            // This replaces the functionality of post.aspx
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            ViewBag.PostId = id;
            return View();
        }

        public IActionResult Page(string id)
        {
            // This replaces the functionality of page.aspx
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            ViewBag.PageId = id;
            return View();
        }

        public IActionResult Archive()
        {
            // This replaces the functionality of archive.aspx
            return View();
        }

        public IActionResult Search(string q)
        {
            // This replaces the functionality of search.aspx
            ViewBag.Query = q;
            return View();
        }

        public IActionResult Contact()
        {
            // This replaces the functionality of contact.aspx
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string name, string email, string subject, string message)
        {
            // Handle contact form submission
            // TODO: Implement contact form logic
            ViewBag.Message = "Thank you for your message!";
            return View();
        }

        public IActionResult Error()
        {
            // This replaces the functionality of error.aspx
            return View();
        }

        public IActionResult Error404()
        {
            // This replaces the functionality of error404.aspx
            return View();
        }
    }
}