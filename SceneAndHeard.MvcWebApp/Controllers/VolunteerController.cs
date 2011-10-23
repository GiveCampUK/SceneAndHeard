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
    public class VolunteerController : Controller
    {
        private SceneCRM context = new SceneCRM();

        //
        // GET: /Volunteer/

        public ViewResult Index()
        {
            return View(context.Volunteers.Include("CrbChecks").Include("Jobs").OrderBy(v => v.Surname).ToList());
        }

        //
        // GET: /Volunteer/Details/5

        public ViewResult Details(int id)
        {
            Volunteer volunteer = context.Volunteers.Single(x => x.VolunteerId == id);
            return View(volunteer);
        }

        //
        // GET: /Volunteer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Volunteer/Create

        [HttpPost]
        public ActionResult Create(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                context.Volunteers.AddObject(volunteer);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(volunteer);
        }
        
        //
        // GET: /Volunteer/Edit/5
 
        public ActionResult Edit(int id)
        {
            Volunteer volunteer = context.Volunteers.Single(x => x.VolunteerId == id);
            return View(volunteer);
        }

        //
        // POST: /Volunteer/Edit/5

        [HttpPost]
        public ActionResult Edit(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                context.Volunteers.Attach(volunteer);
                context.ObjectStateManager.ChangeObjectState(volunteer, EntityState.Modified);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteer);
        }


        //
        // POST: /Volunteer/Edit/5

        [HttpPost]
        public ActionResult Search(string searchPhrase)
        {
            var matches = context.Volunteers.Where(v => v.FirstName.Contains(searchPhrase) || v.Surname.Contains(searchPhrase));

            return View("Index",matches);
        }

        //
        // GET: /Volunteer/Delete/5
 
        public ActionResult Delete(int id)
        {
            Volunteer volunteer = context.Volunteers.Single(x => x.VolunteerId == id);
            return View(volunteer);
        }

        //
        // POST: /Volunteer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer volunteer = context.Volunteers.Single(x => x.VolunteerId == id);
            context.Volunteers.DeleteObject(volunteer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Eligible(bool? IsEligible)
        {
            
            
            var volunteers = context.Volunteers
                                    .Where(v => v.AvailableFrom.HasValue && v.AvailableFrom <= DateTime.Today)
                                    .ToList()
                                    .Where(v => IsEligible.Value);
            return View("EligibleVolunteers", volunteers);
        }

        [HttpGet]
        public JsonResult AllVolunteers(string pleaseWork)
        {
            var matches = context.Volunteers.Where(v => v.FirstName.StartsWith(pleaseWork) || v.Surname.StartsWith(pleaseWork))
                                            .OrderBy(v => v.Surname)
                                            .Select(v => new { Id = v.VolunteerId, Name = v.FirstName + " " + v.Surname });

            return Json(matches, JsonRequestBehavior.AllowGet);
        }


    }
}