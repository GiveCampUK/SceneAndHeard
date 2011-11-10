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
    public class CRBController : Controller
    {
        private SceneCRM db = new SceneCRM();

        //
        // GET: /CRB/

        public ViewResult Index()
        {
            var crbchecks = db.CrbChecks.Include("Volunteer");
            return View(crbchecks.ToList());
        }

        //
        // GET: /CRB/Details/5

        public ViewResult Details(int id)
        {
            CrbCheck crbcheck = db.CrbChecks.Single(c => c.CrbCheckId == id);
            return View(crbcheck);
        }

        //
        // GET: /CRB/Create

        public ActionResult Create(int id)
        {
            ViewBag.Volunteer = db.Volunteers.FirstOrDefault(v => v.VolunteerId == id);

            return View(new CrbCheck { VolunteerId = id});
        } 

        //
        // POST: /CRB/Create

        [HttpPost]
        public ActionResult Create(CrbCheck crbcheck)
        {
            if (ModelState.IsValid)
            {
                db.CrbChecks.Add(crbcheck);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.VolunteerId = new SelectList(db.Volunteers, "VolunteerId", "FirstName", crbcheck.VolunteerId);
            return View(crbcheck);
        }
        
        //
        // GET: /CRB/Edit/5
 
        public ActionResult Edit(int id)
        {
            CrbCheck crbcheck = db.CrbChecks.Single(c => c.CrbCheckId == id);
            ViewBag.VolunteerId = new SelectList(db.Volunteers, "VolunteerId", "FirstName", crbcheck.VolunteerId);
            return View(crbcheck);
        }

        //
        // POST: /CRB/Edit/5

        [HttpPost]
        public ActionResult Edit(CrbCheck crbcheck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crbcheck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VolunteerId = new SelectList(db.Volunteers, "VolunteerId", "FirstName", crbcheck.VolunteerId);
            return View(crbcheck);
        }

        //
        // GET: /CRB/Delete/5
 
        public ActionResult Delete(int id)
        {
            CrbCheck crbcheck = db.CrbChecks.Single(c => c.CrbCheckId == id);
            return View(crbcheck);
        }

        //
        // POST: /CRB/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CrbCheck crbcheck = db.CrbChecks.Single(c => c.CrbCheckId == id);
            db.CrbChecks.Remove(crbcheck);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}