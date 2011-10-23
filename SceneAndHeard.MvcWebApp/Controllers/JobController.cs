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
    public class JobController : Controller
    {
        private SceneCRM context = new SceneCRM();

        //
        // GET: /Job/

        public ViewResult Index()
        {
            return View(context.Jobs.Include("Volunteers").ToList());
        }

        //
        // GET: /Job/Details/5

        public ViewResult Details(int id)
        {
            Job job = context.Jobs.Single(x => x.JobId == id);
            var v = new List<Volunteer>();
            foreach (var cv in job.CourseVolunteers.Where(cv => !v.Contains(cv.Volunteer)))
            {
                v.Add(cv.Volunteer);
            }

            foreach (var cv in job.PlayVolunteers.Where(cv => !v.Contains(cv.Volunteer)))
            {
                v.Add(cv.Volunteer);
            }

            foreach (var cv in job.ProductionVolunteers.Where(cv => !v.Contains(cv.Volunteer)))
            {
                v.Add(cv.Volunteer);
            }

            ViewBag.CurrentVolunteers = v.OrderBy(x => x.Name);
            




            return View(job);
        }

        //
        // GET: /Job/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Job/Create

        [HttpPost]
        public ActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                context.Jobs.AddObject(job);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        //
        // GET: /Job/Edit/5

        public ActionResult Edit(int id)
        {
            Job job = context.Jobs.Single(x => x.JobId == id);
            return View(job);
        }

        //
        // POST: /Job/Edit/5

        [HttpPost]
        public ActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                context.Jobs.Attach(job);
                context.ObjectStateManager.ChangeObjectState(job, EntityState.Modified);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        //
        // GET: /Job/Delete/5

        public ActionResult Delete(int id)
        {
            Job job = context.Jobs.Single(x => x.JobId == id);
            return View(job);
        }

        //
        // POST: /Job/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = context.Jobs.Single(x => x.JobId == id);
            context.Jobs.DeleteObject(job);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}