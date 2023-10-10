using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.AspNetCore.Http;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ForumContext _context;

        public QuestionController(ForumContext context)
        {
            _context = context;
        }



        // GET: all Question for details 
        public async Task<IActionResult> Index()
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");
            var forumContext = _context.QuestionModel.ToListAsync();
            return View(await forumContext);
        }


        public async Task<IActionResult> LeursQuestions(int? id)
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

            if (id == null || _context.QuestionModel == null)
            {
                return NotFound();
            }
            var forumContext = await _context.QuestionModel.Where(x => x.UserId == id).ToListAsync();

                if (forumContext == null) return NotFound();

                return View(forumContext);
          
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> MesQuestions( ) {
            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                var forumContext = await _context.QuestionModel
              .Where(x => x.UserId == HttpContext.Session.GetInt32("iduser")).ToListAsync();

                if (forumContext == null) return NotFound();

                return View(forumContext);
            }
            return RedirectToAction("Connexion", "user");
          

        }


        // GET: all Question for edit delete 
        public async Task<IActionResult> Admin()
        {
            var forumContext = _context.QuestionModel.Include(q => q.User);
            return View(await forumContext.ToListAsync());
        }


        // GET: QuestionModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

            if (id == null || _context.QuestionModel == null)
            {
                return NotFound();
            }

            var questionModel = await _context.QuestionModel
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionModel == null)
            {
                return NotFound();
            }

            return View(questionModel);
        }

        [HttpGet]
        // GET: QuestionModels/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                //ViewData["UserId"] = new SelectList(_context.Set<UserModel>(), "ID", "ID");
                ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

                return View();
            }
            return RedirectToAction("Connexion", "user");
        }

        // POST: QuestionModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public async Task<IActionResult> Create([Bind("Id,UserId,Titre,Topic,Description")] QuestionModel questionModel)
        {
            //if (ModelState.IsValid) {

                _context.QuestionModel.Add(questionModel);
                await _context.SaveChangesAsync();          //}


            return RedirectToAction(nameof(Index));
            // ViewData["UserId"] = new SelectList(_context.Set<UserModel>(), "ID", "ID", questionModel.UserId);
            //  return View(questionModel);
        }

        // GET: QuestionModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                if (id == null || _context.QuestionModel == null)
                {
                    return NotFound();
                }
                ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");
                ViewData["idquestion"] = id;

                var questionModel = await _context.QuestionModel.FindAsync(id);

                if (questionModel == null)
                {
                    return NotFound();
                }
                //  ViewData["UserId"] = new SelectList(_context.Set<UserModel>(), "ID", "ID", questionModel.UserId);

                if (questionModel.UserId == HttpContext.Session.GetInt32("iduser"))
                return View(questionModel);

            }
            return RedirectToAction("Connexion", "user");
           
        }


        // POST: QuestionModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Titre,Topic,Description")] QuestionModel questionModel)
        {
            if (id != questionModel.Id)
            {
                return NotFound();
            }

           // if (ModelState.IsValid) {
                try
                {
                    _context.Update(questionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionModelExists(questionModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
          //  }
           // ViewData["UserId"] = new SelectList(_context.Set<UserModel>(), "ID", "ID", questionModel.UserId);
            return View(questionModel);
        }

        // GET: QuestionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QuestionModel == null)
            {
                return NotFound();
            }

            var questionModel = await _context.QuestionModel
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionModel == null)
            {
                return NotFound();
            }

            return View(questionModel);
        }

        // POST: QuestionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QuestionModel == null)
            {
                return Problem("Entity set 'ForumContext.QuestionModel'  is null.");
            }
            var questionModel = await _context.QuestionModel.FindAsync(id);
            if (questionModel != null)
            {
                _context.QuestionModel.Remove(questionModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionModelExists(int id)
        {
          return (_context.QuestionModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
