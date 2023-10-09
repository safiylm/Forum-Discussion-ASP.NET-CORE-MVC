using Forum_descussion_ASP.NET_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

            return View();
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