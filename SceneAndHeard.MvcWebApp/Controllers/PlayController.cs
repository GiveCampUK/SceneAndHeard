using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SceneAndHeard.Support;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers
{
    public class PlayController : Controller
    {
        private SceneCRM context = new SceneCRM();

        private readonly InitialisesVolunteerAllocationView _initialisesVolunteerAllocationView =
            new InitialisesVolunteerAllocationView();

        private readonly InterpretsPostedVolunteerAllocations _interpretsPostedVolunteerAllocations =
            new InterpretsPostedVolunteerAllocations();

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

            _initialisesVolunteerAllocationView.Initialise(ViewBag, context);

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

                ApplyAllocatedVolunteers(play);

                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PossibleStudents = context.Students;
            _initialisesVolunteerAllocationView.Initialise(ViewBag, context);

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

            _initialisesVolunteerAllocationView.Initialise(ViewBag, context, 
                play.PlayVolunteers.Select(pv => new VolunteerAllocation(pv.VolunteerId, pv.JobId, pv.Notes)));

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

                ApplyAllocatedVolunteers(play);

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleStudents = context.Students;

            _initialisesVolunteerAllocationView.Initialise(ViewBag, context,
               play.PlayVolunteers.Select(pv => new VolunteerAllocation(pv.VolunteerId, pv.JobId, pv.Notes)));

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

        private void ApplyAllocatedVolunteers(Play play)
        {
            var volunteerAllocations = _interpretsPostedVolunteerAllocations.Interpret(Request.Form);

            play.PlayVolunteers.Clear();

            foreach (var volunteerAllocation in volunteerAllocations)
            {
                play.PlayVolunteers.Add(new PlayVolunteer
                {
                    VolunteerId = volunteerAllocation.VolunteerId,
                    JobId = volunteerAllocation.JobId,
                    Notes = volunteerAllocation.Notes
                });
            }
        }

    }
}