using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForumContext _context;

  
        public HomeController(ILogger<HomeController> logger, ForumContext context)
        {
            _logger = logger;
            _context = context;
        }

  
        // for display in home page 
        public async Task<IActionResult> Index()
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");
            var forumContext = _context.QuestionModel.Include(q => q.User);
            return View(await forumContext.ToListAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("iduser");

            return RedirectToAction("Connexion", "user");
        }


    }
}