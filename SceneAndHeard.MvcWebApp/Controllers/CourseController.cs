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
                context.SaveChanges();
                return RedirectToAction("Index");
            }
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
}