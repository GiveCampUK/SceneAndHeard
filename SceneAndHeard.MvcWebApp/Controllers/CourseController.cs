using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers
{
    public class CourseController : Controller
    {
        private SceneCRM context = new SceneCRM();

        //
        // GET: /Course/

        public ViewResult Index()
        {
            return View(context.Courses.Include("Term").AsQueryable().OrderByDescending(x => x.CourseId).ToList());
        }

        //
        // GET: /Course/Details/5

        public ViewResult Details(int id)
        {
            Course course = context.Courses.Single(x => x.CourseId == id);
            return View(course);
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCourseTypes = context.CourseTypes;
            ViewBag.PossibleVolunteers = context.Volunteers;
            ViewBag.PossibleJobs = context.Jobs;
            ViewBag.PossibleTerms = context.Terms;
            return View();
        }

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Courses.AddObject(course);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PossibleVolunteers = context.Volunteers;
            ViewBag.PossibleJobs = context.Jobs;
            ViewBag.PossibleTerms = context.Terms;

            return View(course);
        }

        //
        // GET: /Course/Edit/5

        public ActionResult Edit(int id)
        {
            Course course = context.Courses.Single(x => x.CourseId == id);
            ViewBag.PossibleCourseTypes = context.CourseTypes;
            ViewBag.PossibleTerms = context.Terms;
            ViewBag.PossibleVolunteers = context.Volunteers;
            ViewBag.PossibleJobs = context.Jobs;

            return View(course);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Courses.Attach(course);
                context.ObjectStateManager.ChangeObjectState(course, EntityState.Modified);

                var allocatedVolunteers = new InterpretsPostedVolunteerAllocations().Interpret(Request.Form);

                course.CourseVolunteers.Clear();
                foreach (var volunteerAllocation in allocatedVolunteers)
                {                    
                    course.CourseVolunteers.Add(
                        new CourseVolunteer
                            {
                                JobId = volunteerAllocation.JobId,
                                VolunteerId = volunteerAllocation.VolunteerId, 
                                Notes = volunteerAllocation.Notes
                            });
                }
                
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleVolunteers = context.Volunteers;
            ViewBag.PossibleJobs = context.Jobs;
            ViewBag.PossibleTerms = context.Terms;
            return View(course);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id)
        {
            Course course = context.Courses.Single(x => x.CourseId == id);
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = context.Courses.Single(x => x.CourseId == id);
            context.Courses.DeleteObject(course);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    public class InterpretsPostedVolunteerAllocations
    {


        public IEnumerable<VolunteerAllocation> Interpret(NameValueCollection form)
        {
            var keyNumbers =
                form.AllKeys
                    .Where(key => key.StartsWith("volunteer", StringComparison.InvariantCultureIgnoreCase))
                    .Select(key => key.Substring(9));

            foreach (var keyNumber in keyNumbers)
            {
                var volunteerIdString = form[string.Format("volunteer{0}", keyNumber)];
                var jobIdString = form[string.Format("job{0}", keyNumber)];
                var notes = form[string.Format("notes{0}", keyNumber)];

                if (volunteerIdString != null && jobIdString != null)
                {
                    int volunteerId;
                    int jobId;

                    if (int.TryParse(volunteerIdString, out volunteerId) && int.TryParse(jobIdString, out jobId))
                        yield return new VolunteerAllocation(volunteerId, jobId, notes);
                }

            }

        }
    }

    public class VolunteerAllocation
    {
        public VolunteerAllocation(int volunteerId, int jobId, string notes)
        {
            VolunteerId = volunteerId;
            JobId = jobId;
            Notes = notes;
        }

        public int VolunteerId { get; private set; }
        public int JobId { get; private set; }
        public string Notes { get; private set; }
    }
}