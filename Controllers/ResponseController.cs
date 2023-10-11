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
        public async Task<IActionResult> Index()
        {
            var forumContext = _context.ResponseModel;
            return View(await forumContext.ToListAsync());
        }

        // GET: Response/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResponseModel == null)
            {
                return NotFound();
            }

            var responseModel = await _context.ResponseModel
             //   .Include(r => r.Question)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (responseModel == null)
            {
                return NotFound();
            }

            return View(responseModel);
        }

        // GET: Response/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.QuestionModel, "Id", "Id");
            return View();
        }

        // POST: Response/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResponseContent")] ResponseModel res)
        {
            //  if (ModelState.IsValid){
            ResponseModel responseModel = new ResponseModel
            {
                UserId= 5, 
                QuestionId=5,
                ResponseContent="aller on va y arriver !!!!!!",
                DateCreation = DateTime.Now,

            };

                _context.ResponseModel.Add(responseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
          //  ViewData["QuestionId"] = new SelectList(_context.QuestionModel, "Id", "Id", responseModel.QuestionId);
            return View(responseModel);
        }

        // GET: Response/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResponseModel == null)
            {
                return NotFound();
            }

            var responseModel = await _context.ResponseModel.FindAsync(id);
            if (responseModel == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.QuestionModel, "Id", "Id", responseModel.QuestionId);
            return View(responseModel);
        }

        // POST: Response/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserId,QuestionId,ResponseContent,DateCreation")] ResponseModel responseModel)
        {
            if (id != responseModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponseModelExists(responseModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.QuestionModel, "Id", "Id", responseModel.QuestionId);
            return View(responseModel);
        }

        // GET: Response/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResponseModel == null)
            {
                return NotFound();
            }

            var responseModel = await _context.ResponseModel
               // .Include(r => r.Question)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (responseModel == null)
            {
                return NotFound();
            }

            return View(responseModel);
        }

        // POST: Response/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResponseModel == null)
            {
                return Problem("Entity set 'ForumContext.ResponseModel'  is null.");
            }
            var responseModel = await _context.ResponseModel.FindAsync(id);
            if (responseModel != null)
            {
                _context.ResponseModel.Remove(responseModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponseModelExists(int id)
        {
          return (_context.ResponseModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
