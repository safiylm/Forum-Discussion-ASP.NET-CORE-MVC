using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class ResponseController : Controller
    {
        private readonly ForumContext _context;

        public ResponseController(ForumContext context)
        {
            _context = context;
        }

        // GET: Response
        public async Task<IActionResult> Index(int? id)
        {
            var forumContext = _context.ResponseModel.Include(r => r.Question).
                Include(r => r.User).Where(x => x.QuestionId == id).ToListAsync();
            return View(await forumContext);
        }

        // GET: Response/Create
        public IActionResult Create(int? id)
        {
         
                ViewData["idquestion"] = id;

                ViewData["QuestionId"] = new SelectList(_context.QuestionModel, "Id", "Id");
                ViewData["UserId"] = new SelectList(_context.UserModel, "Id", "Id");
                return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,ResponseContent")] ResponseModel responseModel)
        {
            responseModel.UserId = (int)HttpContext.Session.GetInt32("iduser");
            responseModel.DateCreation = DateTime.Now;
            if (responseModel != null)
            {
                _context.Add(responseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("details", "question", new { id = responseModel.QuestionId });
            }

            ViewData["QuestionId"] = new SelectList(_context.QuestionModel, "Id", "Id", responseModel.QuestionId);
            ViewData["UserId"] = new SelectList(_context.UserModel, "Id", "Id", responseModel.UserId);
            return View(responseModel);
        }

        public async Task<IActionResult> EditPartial(int? id)
        {
            if (id == null || _context.ResponseModel == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                var responseModel = await _context.ResponseModel.FindAsync(id);
                if (responseModel == null)
                {
                    return NotFound();
                }
                ViewData["ResponseId"] =id;
                ViewData["ResponseContent"] = responseModel.ResponseContent;

                ViewData["QuestionId"] = responseModel.QuestionId;
                //ViewData["QuestionId"] = new SelectList(_context.QuestionModel, "Id", "Id", responseModel.QuestionId);
                ViewData["UserId"] = HttpContext.Session.GetInt32("iduser");
                return PartialView(responseModel);
            }
            return RedirectToAction("Connexion", "user");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,QuestionId,ResponseContent,DateCreation")] ResponseModel responseModel)
        {
            if (id != responseModel.Id)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                try
                {
                    _context.ResponseModel.Update(responseModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Question", new { id = responseModel.QuestionId });

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponseModelExists(responseModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }

            return RedirectToAction("Connexion", "User");

           // ViewData["QuestionId"] = new SelectList(_context.QuestionModel, "Id", "Id", responseModel.QuestionId);
           // ViewData["UserId"] = new SelectList(_context.UserModel, "Id", "Id", responseModel.UserId);
           // return View(responseModel);
        }

        public async Task<IActionResult> DeletePartial(int? id)
        {
            if (id == null || _context.ResponseModel == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetInt32("iduser") != null)
            {

                var responseModel = await _context.ResponseModel
                .Include(r => r.Question)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
                ViewData["QuestionId"] = responseModel.QuestionId;

                if (responseModel == null)
            {
                return NotFound();
            }

            return PartialView(responseModel);
            }

            return RedirectToAction("Connexion", "user");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResponseModel == null)
            {
                return Problem("Entity set 'ForumContext.ResponseModel'  is null.");
            }
            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                var responseModel = await _context.ResponseModel.FindAsync(id);
            if (responseModel != null)
            {
                _context.ResponseModel.Remove(responseModel);
            }
            
            await _context.SaveChangesAsync();
                return RedirectToAction("details", "question", new { id = responseModel.QuestionId });
            }

            return RedirectToAction("Connexion", "user");
        }

        private bool ResponseModelExists(int id)
        {
          return (_context.ResponseModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
