﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;
using Microsoft.AspNetCore.Http;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly ForumContext _context;

        public UserController(ForumContext context)
        {
            _context = context;
        }


        // GET: Inscription
        [HttpGet]
        public IActionResult Inscription()
        {

          //  HttpContext.Session.SetString("Email", "safinazzzzz");
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Mesquestions", "Question");

            }
        }



        [HttpPost]
        public async Task<IActionResult> Inscription(UserModel userModel)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                if (userModel != null)
                {
                    
                     _context.UserModel.Add(userModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Connexion");

                }
               
            }else
            {
                return RedirectToAction("index", "Question");

            }
            return View();
        }

      



        [HttpGet]
        public IActionResult Connexion()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Mesquestions", "Question");
            }
        }


        public async Task<IActionResult> Connexion(UserModel userModel)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
              
                if (userModel == null || _context.UserModel == null)
                {
                    return NotFound();
                }

                var user = await _context.UserModel
                    .FirstOrDefaultAsync(m => m.Email == userModel.Email) ;

                if (user == null)
                {
                    return NotFound();
                }
                else if (user.Password == userModel.Password)
                {
                    HttpContext.Session.SetInt32("iduser", user.ID);
                    HttpContext.Session.SetString("NameUser", user.NameUser);
                    HttpContext.Session.SetString("Email", userModel.Email);
                    HttpContext.Session.SetString("Password", userModel.Password);


                }

            }
            return RedirectToAction("index", "Question");
        }



        // GET: UserModels/Index/5
        public async Task<IActionResult> Credential()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                if ( _context.UserModel == null)
                {
                    return NotFound();
                }

                var userModel = await _context.UserModel
                    .FirstOrDefaultAsync(m => m.ID == HttpContext.Session.GetInt32("iduser") );
                if (userModel == null)
                {
                    return NotFound();
                }

                if(userModel.Email== HttpContext.Session.GetString("Email"))
                    return View(userModel);
            }
            return RedirectToAction("Connexion");


        }

        public async Task<IActionResult> List()
        {
           
            return _context.UserModel != null ?
                 View(await _context.UserModel.ToListAsync()) :
                   Problem("Entity set 'ForumContext.UserModel'  is null.");
        
          

        }


        // GET: UserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Email") != null)
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
                if (userModel.Email == HttpContext.Session.GetString("Email"))

                    return View(userModel);

            }
            return RedirectToAction("Connexion");
    }



        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NameUser,Email,Password")] UserModel userModel)
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                if (id != userModel.ID)
            {
                return NotFound();
            }

                //    if (ModelState.IsValid)//  {
                try
                {
                    if (userModel.ID == HttpContext.Session.GetInt32("iduser"))
                    {    _context.Update(userModel);
                        await _context.SaveChangesAsync();
                    }
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
                //  }
                    return View(userModel);
            }
            return RedirectToAction("Connexion");
        }


        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Email") != null)
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
                if (userModel.ID == HttpContext.Session.GetInt32("iduser"))

                    return View(userModel);
            }
            return RedirectToAction("Connexion");
        }


        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("Email") != null)
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
                if (userModel.ID == HttpContext.Session.GetInt32("iduser"))
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
            }
            return RedirectToAction("Connexion");
        }

        private bool UserModelExists(int id)
        {
          return (_context.UserModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
