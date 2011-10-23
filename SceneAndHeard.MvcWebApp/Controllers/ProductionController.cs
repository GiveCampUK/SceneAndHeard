using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using SceneAndHeard.Support;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers
{
    public class ProductionController : Controller
    {
        private SceneCRM db = new SceneCRM();

        private InitialisesVolunteerAllocationView _initialisesVolunteerAllocationView =
            new InitialisesVolunteerAllocationView();

        private InterpretsPostedVolunteerAllocations _interpretsPostedVolunteerAllocations =
            new InterpretsPostedVolunteerAllocations();

        //
        // GET: /Production/
        [Authorize]
        public ViewResult Index()
        {
            return View(db.Productions.ToList());
        }

        //
        // GET: /Production/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            Production production = db.Productions.Single(p => p.ProductionId == id);
            return View(production);
        }

        //
        // GET: /Production/Create
        [Authorize]
        public ActionResult Create()
        {
            _initialisesVolunteerAllocationView.Initialise(ViewBag, db);

            return View();
        }

        //
        // POST: /Production/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Production production)
        {
            if (ModelState.IsValid)
            {
                db.Productions.AddObject(production);

                ApplyProductionVolunteerAllocations(production);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(production);
        }

        //
        // GET: /Production/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Production production = db.Productions.Single(p => p.ProductionId == id);

            _initialisesVolunteerAllocationView.Initialise(ViewBag, db, production.ProductionVolunteers
                .Select(pv => new VolunteerAllocation(pv.VolunteerId, pv.JobId, pv.Notes)));

            return View(production);
        }

        //
        // POST: /Production/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Production production)
        {
            if (ModelState.IsValid)
            {
                db.Productions.Attach(production);
                db.ObjectStateManager.ChangeObjectState(production, EntityState.Modified);

                ApplyProductionVolunteerAllocations(production);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(production);
        }

        //
        // GET: /Production/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Production production = db.Productions.Single(p => p.ProductionId == id);
            return View(production);
        }

        //
        // POST: /Production/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Production production = db.Productions.Single(p => p.ProductionId == id);
            db.Productions.DeleteObject(production);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void ApplyProductionVolunteerAllocations(Production production)
        {
            var volunteerAllocations = _interpretsPostedVolunteerAllocations.Interpret(Request.Form);

            production.ProductionVolunteers.Clear();
            foreach (var volunteerAllocation in volunteerAllocations)
            {
                production.ProductionVolunteers.Add(new ProductionVolunteer
                {
                    VolunteerId = volunteerAllocation.VolunteerId,
                    JobId = volunteerAllocation.JobId,
                    Notes = volunteerAllocation.Notes
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}