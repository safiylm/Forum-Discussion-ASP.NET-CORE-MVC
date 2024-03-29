﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum_descussion_ASP.NET_core_mvc.Data;
using Forum_descussion_ASP.NET_core_mvc.Models;
using Forum_descussion_ASP.NET_core_mvc.Migrations;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ForumContext _context;

        public QuestionController(ForumContext context)
        {
            _context = context;
        }

        // GET: Question
        public async Task<IActionResult> Index()
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");
            var forumContext = _context.QuestionModel.Include(q => q.User);
            return View(await forumContext.ToListAsync());
        }

 


        public async Task<IActionResult> LeursQuestions(int? id)
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");
            ViewData["iduser_compte"] = id;

            if (id == null || _context.QuestionModel == null)
            {
                return NotFound();
            }
            var forumContext = await _context.QuestionModel.Where(x => x.UserId == id).ToListAsync();

            if (forumContext == null) return NotFound();

            return View(forumContext);

        }


        public async Task<IActionResult> MesQuestions()
        {
            if (HttpContext.Session.GetInt32("iduser") != null)
            {
                var forumContext = await _context.QuestionModel
              .Where(x => x.UserId == HttpContext.Session.GetInt32("iduser")).ToListAsync();

                if (forumContext == null) return NotFound();

                return View(forumContext);
            }
            return RedirectToAction("Connexion", "user");


        }





        // GET: Question/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QuestionModel == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetInt32("iduser") == null) ViewData["iduser"] = 0; 
            else  ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");
           
            ViewData["idquestion"] = id;

            var questionModel = await _context.QuestionModel
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (questionModel == null )
            {
                return NotFound();
            }
            ViewData["AuteurId"] = questionModel.UserId;

            ViewData["NameUser"] = questionModel.User.NameUser;
            ViewData["DateCreation"] = questionModel.DateCreation.ToString("dd/MM/yyyy");
            ViewData["Description"] = questionModel.Description;
            ViewData["Topic"] = questionModel.Topic;
            ViewData["Titre"] = questionModel.Titre;
            ViewData["isResolu"] = questionModel.isResolu;
            ViewData["Photo"] = questionModel.User.Photo;
            ViewData["idSolution"] = questionModel.idSolution;

            var forumContext = await _context.ResponseModel.Include(r => r.Question).
                    Include(r => r.User).Where(x => x.QuestionId == id).ToListAsync();
           

            if (forumContext == null)
            {
                return NotFound();
            }
            return View( forumContext);

        }

        

        // GET: Question/Create
        public IActionResult Create()
        {
            ViewData["iduser"] = HttpContext.Session.GetInt32("iduser");

            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Titre,Topic,Description,DateCreation")] QuestionModel questionModel)
        {
            if (HttpContext.Session.GetInt32("iduser") != null)
            {

                _context.QuestionModel.Add(questionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            }
            return RedirectToAction("Connexion", "user");
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
                ViewData["description"] = questionModel.Description;

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

            if (HttpContext.Session.GetInt32("iduser") == questionModel.UserId )
            {
                // if (ModelState.IsValid) {
                try
                {
                    questionModel.DateCreation = DateTime.Now;
                    _context.QuestionModel.Update(questionModel);
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

            }
            return RedirectToAction("Connexion", "User");
        }

        // GET: QuestionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("iduser") != null)
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


                if (questionModel.UserId == HttpContext.Session.GetInt32("iduser"))

                    return View(questionModel);
            }
            return RedirectToAction("connexion", "user");
        }
        [HttpGet]
        public async Task<IActionResult> addQuestionResolu(int idQuestion, int idResponseSolution) {


            if (idQuestion == 0 && idResponseSolution == 0)
            {
                return NotFound();
            }

            var questionModel = await _context.QuestionModel.FindAsync(idQuestion) ;

            // if (ModelState.IsValid) {
            try
            {
                questionModel.idSolution = idResponseSolution; 
                questionModel.isResolu = true;

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

            return RedirectToAction("Details", "question", new { id = idQuestion });
        }

        [HttpGet]
        public async Task<IActionResult> removeQuestionResolu(int idQuestion )
        {


            if (idQuestion == 0)
            {
                return NotFound();
            }

            var questionModel = await _context.QuestionModel.FindAsync(idQuestion);

            // if (ModelState.IsValid) {
            try
            {
                questionModel.idSolution = 0;
                questionModel.isResolu = false; 

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

            return RedirectToAction("Details", "question", new { id = idQuestion });
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
