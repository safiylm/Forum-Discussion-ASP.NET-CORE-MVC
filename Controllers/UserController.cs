﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json.Nodes;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly ForumContext _context;

        public UserController(ForumContext context)
        {
            _context = context;
        }

        public IActionResult UserPartial(int id)
        {
            var forumContext = _context.UserModel.Where(q => q.Id == id );
            return PartialView(forumContext);

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


        [HttpGet]
        public   IActionResult PhotoPartial () {
            return PartialView();                           // do something with myJson
        }

        // GET: Inscription
        [HttpGet]
        public IActionResult MotDePasseOublie()
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
            if (HttpContext.Session.GetInt32("iduser") == null)
            {
                if (userModel != null)
                {
                    userModel.Password = SecretHasher.Hash(userModel.Password);
                    _context.UserModel.Add(userModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Connexion");

                }

            }
            else
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
            if (HttpContext.Session.GetInt32("iduser") == null)
            {

                if (userModel == null || _context.UserModel == null)
                {
                    return NotFound();
                }

                var user = await _context.UserModel
                    .FirstOrDefaultAsync(m => m.Email == userModel.Email);


                if (user == null)
                {
                    return NotFound();
                }

                else if (SecretHasher.Verify(userModel.Password, user.Password ) )
                {
                    HttpContext.Session.SetInt32("iduser", user.Id);
                    HttpContext.Session.SetString("NameUser", user.NameUser);
                    HttpContext.Session.SetString("Email", userModel.Email);
                    HttpContext.Session.SetString("Password", userModel.Password);


                }

            }
            return RedirectToAction("index", "Question");
        }




        public async Task<IActionResult> Credential()
        {
            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                if (_context.UserModel == null)
                {
                    return NotFound();
                }

                var userModel = await _context.UserModel
                    .FirstOrDefaultAsync(m => m.Id == HttpContext.Session.GetInt32("iduser"));
                if (userModel == null)
                {
                    return NotFound();
                }

                if (userModel.Id == HttpContext.Session.GetInt32("iduser"))
                    return View(userModel);
            }
            return RedirectToAction("Connexion");


        }



        public async Task<IActionResult> List()
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

            return _context.UserModel != null ?
                 View(await _context.UserModel.ToListAsync()) :
                   Problem("Entity set 'ForumContext.UserModel'  is null.");



        }

        public async Task<IActionResult> ListPartial()
        {
            var forumContext = await _context.UserModel.ToListAsync();
            return PartialView(forumContext);

        }

        public IActionResult MenuHorizontalPartial()
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");
            return PartialView();
        }



        [HttpGet]
        // GET: UserModels/Edit/5
        public async Task<IActionResult> EditPartial()
        {
            if (_context.UserModel == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetInt32("iduser") != null)
            {
               
                ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

                var userModel = await _context.UserModel.FindAsync(HttpContext.Session.GetInt32("iduser"));
                if (userModel == null)
                {
                    return NotFound();
                }
                if (userModel.Id == HttpContext.Session.GetInt32("iduser")) //user can see only his data 

                    return PartialView(userModel);

            }
            return RedirectToAction("Connexion");
        }



        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(" Id,NameUser,Email,Password")] UserModel userModel)
        {
         
            if (HttpContext.Session.GetInt32("iduser") != null)
            {

                //    if (ModelState.IsValid)//  {
                if (userModel == null) return NotFound();

                _context.UserModel.Update(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Credential");


                //  }
            }
            return RedirectToAction("Connexion");
        }


        // GET: UserModels/Delete/5
        public async Task<IActionResult> DeletePartial()
        {
            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                if (_context.UserModel == null)
                {
                    return NotFound();
                }

                ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");


                var userModel = await _context.UserModel
                    .FirstOrDefaultAsync(m => m.Id == HttpContext.Session.GetInt32("iduser"));

                if (userModel == null)
                {
                    return NotFound();
                }
                if (userModel.Id == HttpContext.Session.GetInt32("iduser"))

                    return PartialView(userModel);
            }

            return RedirectToAction("Connexion");
        }


        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                if (_context.UserModel == null)
                {
                    return Problem("Entity set 'ForumContext.UserModel'  is null.");
                }

                var userModel = await _context.UserModel.FindAsync(HttpContext.Session.GetInt32("iduser"));
                if (userModel != null && userModel.Id == HttpContext.Session.GetInt32("iduser"))
                {
                    _context.UserModel.Remove(userModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Logout", "Home");
                }
            }
            return RedirectToAction("Connexion");
        }

        private bool UserModelExists(int id)
        {
            return (_context.UserModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
