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
    public class StudentController : Controller
    {
        private SceneCRM context = new SceneCRM();

        //
        // GET: /Student/
        [Authorize]
        public ViewResult Index()
        {
            return View(context.Students.AsQueryable().OrderByDescending(x => x.MembershipNumber).ToList());
        }

        //
        // GET: /Student/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            Student student = context.Students.Single(x => x.StudentId == id);
            return View(student);
        }

        //
        // GET: /Student/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Student/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                context.Students.AddObject(student);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        //
        // GET: /Student/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Student student = context.Students.Single(x => x.StudentId == id);
            return View(student);
        }

        //
        // POST: /Student/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                context.Students.Attach(student);
                context.ObjectStateManager.ChangeObjectState(student, EntityState.Modified);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        //
        // GET: /Student/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Student student = context.Students.Single(x => x.StudentId == id);
            return View(student);
        }

        //
        // POST: /Student/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = context.Students.Single(x => x.StudentId == id);
            context.Students.DeleteObject(student);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}