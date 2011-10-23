using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers
{
    public class PlayController : Controller
    {
        private SceneCRM context = new SceneCRM();

        //
        // GET: /Play/
        [Authorize]
        public ViewResult Index()
        {
            return View(context.Plays.AsQueryable().OrderByDescending(x => x.PlayId));
        }

        //
        // GET: /Play/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            Play play = context.Plays.SingleOrDefault(x => x.PlayId == id);
            return View(play);
        }

        //
        // GET: /Play/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PossibleStudents = context.Students.AsQueryable().OrderBy(x => x.Forename).ThenBy(x => x.Surname);
            ViewBag.PossibleProductions = context.Productions.AsQueryable().OrderBy(x => x.Title);
            return View();
        }

        //
        // POST: /Play/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Play play)
        {
            if (ModelState.IsValid)
            {
                context.Plays.AddObject(play);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PossibleStudents = context.Students;
            return View(play);
        }

        //
        // GET: /Play/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Play play = context.Plays.Single(x => x.PlayId == id);
            ViewBag.PossibleStudents = context.Students;
            ViewBag.PossibleProductions = context.Productions.AsQueryable().OrderBy(x => x.Title);
            return View(play);
        }

        //
        // POST: /Play/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Play play)
        {
            if (ModelState.IsValid)
            {
                context.Plays.Attach(play);
                context.ObjectStateManager.ChangeObjectState(play, EntityState.Modified);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleStudents = context.Students;
            return View(play);
        }

        //
        // GET: /Play/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Play play = context.Plays.Single(x => x.PlayId == id);
            return View(play);
        }

        //
        // POST: /Play/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Play play = context.Plays.Single(x => x.PlayId == id);
            context.Plays.DeleteObject(play);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}