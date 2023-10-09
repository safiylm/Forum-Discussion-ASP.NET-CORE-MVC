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
    public class MesDonneesPersonnellesController : Controller
    {
        private readonly ForumContext _context;

        public MesDonneesPersonnellesController(ForumContext context)
        {
            _context = context;
        }

      



        // GET: UserModels/Index/5
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null || _context.UserModel == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

    

        // GET: UserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserModel == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }



        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NameUser,Email,Password")] UserModel userModel)
        {
            if (id != userModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.ID))
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
            return View(userModel);
        }


        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserModel == null)
            {
                return NotFound();
            }

            var userModel = await _context.UserModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserModel == null)
            {
                return Problem("Entity set 'ForumContext.UserModel'  is null.");
            }
            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel != null)
            {
                _context.UserModel.Remove(userModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
          return (_context.UserModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
