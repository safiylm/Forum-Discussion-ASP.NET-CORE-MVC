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
    public class AuthentificationController : Controller
    {
        private readonly ForumContext _context;

        public AuthentificationController(ForumContext context)
        {
            _context = context;
        }
        
        // GET: UserModels/Create
        public IActionResult Inscription()
        {
            return View();
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insciption([Bind("ID,NameUser,Email,Password")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }



        public IActionResult Connexion()
        {
            return View();
        }

    }
}
