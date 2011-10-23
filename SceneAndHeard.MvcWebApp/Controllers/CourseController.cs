using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SceneAndHeard.Support;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers {
    [Authorize(Roles = "Editor")]
    public class CourseController : Controller {
        private SceneCRM context = new SceneCRM();

        private readonly InitialisesVolunteerAllocationView _initialisesVolunteerAllocationView = new InitialisesVolunteerAllocationView();
        private readonly InterpretsPostedVolunteerAllocations _interpretsPostedVolunteerAllocations = new InterpretsPostedVolunteerAllocations();

        //
        // GET: /Course/
        [Authorize]
        public ViewResult Index() {
            return View(context.Courses.Include("Term").AsQueryable().OrderByDescending(x => x.CourseId).ToList());
        }

        //
        // GET: /Course/Details/5
        [Authorize]
        public ViewResult Details(int id) {
            Course course = context.Courses.Single(x => x.CourseId == id);
            return View(course);
        }

        //
        // GET: /Course/Create
        [Authorize]
        public ActionResult Create() {
            ViewBag.PossibleCourseTypes = context.CourseTypes;
            ViewBag.PossibleTerms = context.Terms;
            _initialisesVolunteerAllocationView.Initialise(ViewBag, context);

            return View();
        }

        //
        // POST: /Course/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Course course) {
            if (ModelState.IsValid) {
                context.Courses.AddObject(course);
                ApplyCourseVolunteerAllocations(course);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            _initialisesVolunteerAllocationView.Initialise(ViewBag, context);
            ViewBag.PossibleTerms = context.Terms;

            return View(course);
        }

        //
        // GET: /Course/Edit/5
        [Authorize]
        public ActionResult Edit(int id) {
            Course course = context.Courses.Single(x => x.CourseId == id);
            ViewBag.PossibleCourseTypes = context.CourseTypes;
            ViewBag.PossibleTerms = context.Terms;

            _initialisesVolunteerAllocationView.Initialise(ViewBag, context, course.CourseVolunteers.Select(cv => new VolunteerAllocation(cv.VolunteerId, cv.JobId, cv.Notes)));

            return View(course);
        }

        //
        // POST: /Course/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Course course) {
            if (ModelState.IsValid) {
                context.Courses.Attach(course);
                context.ObjectStateManager.ChangeObjectState(course, EntityState.Modified);

                ApplyCourseVolunteerAllocations(course);

                context.SaveChanges();
                return RedirectToAction("Index");
            }

            _initialisesVolunteerAllocationView.Initialise(ViewBag, context, course.CourseVolunteers.Select(cv => new VolunteerAllocation(cv.VolunteerId, cv.JobId, cv.Notes)));

            ViewBag.PossibleTerms = context.Terms;
            return View(course);
        }

        private void ApplyCourseVolunteerAllocations(Course course) {
            var allocatedVolunteers = _interpretsPostedVolunteerAllocations.Interpret(Request.Form);

            course.CourseVolunteers.Clear();
            foreach (var volunteerAllocation in allocatedVolunteers) {
                course.CourseVolunteers.Add(
                    new CourseVolunteer {
                        JobId = volunteerAllocation.JobId,
                        VolunteerId = volunteerAllocation.VolunteerId,
                        Notes = volunteerAllocation.Notes
                    });
            }
        }

        //
        // GET: /Course/Delete/5
        [Authorize]
        public ActionResult Delete(int id) {
            Course course = context.Courses.Single(x => x.CourseId == id);
            return View(course);
        }

        //
        // POST: /Course/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Course course = context.Courses.Single(x => x.CourseId == id);
            context.Courses.DeleteObject(course);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST Course/Students/5
        [Authorize]
        [HttpPost]
        public ActionResult Students(int id, string verb, int[] possibleStudents, int[] allocatedStudents) {
            var course = context.Courses.FirstOrDefault(c => c.CourseId == id);
            if (course != default(Course)) {
                switch (verb) {
                    case "Add":
                        foreach (var studentId in possibleStudents) {
                            course.CourseAttendances.Add(new CourseAttendance() {
                                Student = context.Students.First(s => s.StudentId == studentId),
                                Course = course
                            });
                        }
                        context.SaveChanges();
                        break;
                    case "Remove":
                        course.CourseAttendances.Where(ca => allocatedStudents.Contains(ca.StudentId)).ToList().ForEach(ca => course.CourseAttendances.Remove(ca));
                        context.SaveChanges();
                        break;
                }
            }
            return (Students(id));
        }
        [Authorize]
        public ActionResult Students(int id) {
            var course = context.Courses.Single(x => x.CourseId == id);

            var possibleStudents = context.Students.OrderBy(s => s.Forename).ToList();
            var allocatedStudents = course.CourseAttendances.Select(ca => ca.Student).OrderBy(s => s.Forename);

            foreach (var allocatedStudent in allocatedStudents) {
                var possibleStudent = possibleStudents.FirstOrDefault(x => x.StudentId == allocatedStudent.StudentId);

                if (possibleStudent != null)
                    possibleStudents.Remove(possibleStudent);
            }

            ViewBag.PossibleStudents = possibleStudents;
            ViewBag.AllocatedStudents = allocatedStudents;

            return View(course);
        }
    }
}