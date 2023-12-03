using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class EnregistrementController : Controller
    {
        private readonly ForumContext _context;

        public EnregistrementController(ForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");
                var forumContext = _context.EnregistrementModel.Where(q => q.UserId == HttpContext.Session.GetInt32("iduser"));
                return View(await forumContext.ToListAsync());

            }
            return RedirectToAction("Connexion", "User");

        }


        public IActionResult UserPartial()
        {
            var forumContext = _context.UserModel.Where(q => q.Id == HttpContext.Session.GetInt32("iduser"));
            return PartialView(forumContext);

        }


        public IActionResult QuestionPartial(int id, int idEnregistrement)
        {
            ViewData["idEnregistrement"] = idEnregistrement;
            var forumContext = _context.QuestionModel.Where(q => q.Id == id);
            return PartialView(forumContext);

        }

   
        public IActionResult ResponsePartial(int id, int idEnregistrement)
        {
            ViewData["idEnregistrement"] = idEnregistrement;

            var forumContext = _context.ResponseModel.Where(q => q.Id == id);
            return PartialView(forumContext);

        }

        [HttpGet]
        public async Task<IActionResult> postDeleteEnregistrement(int idEnregistrement)
        {
            if (_context.EnregistrementModel == null)
            {
                return Problem("Entity set 'ForumContext.EnregistrementModel'  is null.");
            }
            var enregistrement = await _context.EnregistrementModel.FindAsync(idEnregistrement);
            if (enregistrement != null)
            {
                _context.EnregistrementModel.Remove(enregistrement);
            }

            await _context.SaveChangesAsync(); return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> postEnregistrerReponse([Bind("Id, UserId, QuestionId, ResponseId")] EnregistrementModel enregtmt)
        {
            if (HttpContext.Session.GetInt32("iduser") != null
                && enregtmt.UserId != 0
                && enregtmt.QuestionId != 0
                && enregtmt.ResponseId != 0)
            {
                _context.EnregistrementModel.Add(enregtmt);
                await _context.SaveChangesAsync();

                // return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Connexion", "User");
        }



        [HttpPost]
        public async Task<IActionResult> postEnregistrerQuestion([Bind("Id, UserId, QuestionId")] EnregistrementModel enregtmt)
        {
            if (HttpContext.Session.GetInt32("iduser") != null
                 && enregtmt.UserId != 0
                 && enregtmt.QuestionId != 0
                )
            {
                _context.EnregistrementModel.Add(enregtmt);
                await _context.SaveChangesAsync();

                // return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Connexion", "User");
        }


    }
}
