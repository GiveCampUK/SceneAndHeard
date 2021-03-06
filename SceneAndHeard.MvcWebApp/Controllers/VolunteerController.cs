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
    [Authorize(Roles = "Editor")]
    public class VolunteerController : Controller
    {
        private SceneCRM context = new SceneCRM();

        //
        // GET: /Volunteer/
        [Authorize]
        public ViewResult Index()
        {
            return View(context.Volunteers.Include("CrbChecks").Include("Jobs").OrderBy(v => v.Surname).ToList());
        }

        //
        // GET: /Volunteer/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            Volunteer volunteer = context.Volunteers.Single(x => x.VolunteerId == id);
            return View(volunteer);
        }

        //
        // GET: /Volunteer/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Volunteer/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                context.Volunteers.Add(volunteer);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(volunteer);
        }
        
        //
        // GET: /Volunteer/Edit/5
 [Authorize]
        public ActionResult Edit(int id)
        {
            Volunteer volunteer = context.Volunteers.Single(x => x.VolunteerId == id);
            return View(volunteer);
        }

        //
        // POST: /Volunteer/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                context.Entry(volunteer).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteer);
        }


        //
        // POST: /Volunteer/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Search(string searchPhrase)
        {
            var matches = context.Volunteers.Where(v => v.FirstName.Contains(searchPhrase) || v.Surname.Contains(searchPhrase));

            return View("Index",matches);
        }

        //
        // GET: /Volunteer/Delete/5
 [Authorize]
        public ActionResult Delete(int id)
        {
            Volunteer volunteer = context.Volunteers.Single(x => x.VolunteerId == id);
            return View(volunteer);
        }

        //
        // POST: /Volunteer/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer volunteer = context.Volunteers.Single(x => x.VolunteerId == id);
            context.Volunteers.Remove(volunteer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Eligible(bool? isEligible)
        {
            var volunteers = context.Volunteers
                                    .Where(v => v.AvailableFrom.HasValue && v.AvailableFrom <= DateTime.Today)
                                    .ToList()
                                    .Where(v => isEligible.Value);
            return View("EligibleVolunteers", volunteers);
        }

        [Authorize]
        [HttpGet]
        public JsonResult AllVolunteers(string searchPhrase)
        {
            var matches = context.Volunteers.Where(v => v.FirstName.StartsWith(searchPhrase) || v.Surname.StartsWith(searchPhrase))
                                            .OrderBy(v => v.Surname)
                                            .Select(v => new { Id = v.VolunteerId, Name = v.FirstName + " " + v.Surname });

            return Json(matches, JsonRequestBehavior.AllowGet);
        }
    }
}