
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class TopicController : Controller
    {
        private readonly ForumContext _context;

        public TopicController(ForumContext context)
        {
            _context = context;
        }

    

        // GET: Response
        public IActionResult Index( )
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

            string[] topic_list = { "Programmation-Web", "Developpment-yourself", "Tue", "Wed", "Thu", "Sat" };
            ViewData["topic_list"] = topic_list;
            return View(topic_list );
        }

        public IActionResult ListPartial(string name)
        {
            string[] topic_list = { "Programmation-Web", "Developpment-yourself", "Tue", "Wed", "Thu", "Sat" };
            ViewData["topic_list"] = topic_list;
            ViewData["name"] = name;
            return PartialView(topic_list);

        }

      


        public async Task<IActionResult> TopicName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                RedirectToAction(nameof(Index));
            }

            ViewData["name"] = name;

            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

            var forumContext = _context.QuestionModel.Where(q => q.Topic == name);
            return View(await forumContext.ToListAsync());
        }


    } 

}
