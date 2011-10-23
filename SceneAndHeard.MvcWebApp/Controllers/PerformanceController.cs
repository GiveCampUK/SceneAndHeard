using System.Data;
using System.Linq;
using System.Web.Mvc;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers
{
    public class PerformanceController : Controller
    {
        private SceneCRM db = new SceneCRM();

        //
        // GET: /Performance/
        [Authorize]
        public ViewResult Index()
        {
            var performances = db.Performances.Include("Production");
            return View(performances.ToList());
        }

        //
        // GET: /Performance/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            Performance performance = db.Performances.Single(p => p.PerformanceId == id);
            return View(performance);
        }

        //
        // GET: /Performance/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProductionId = new SelectList(db.Productions, "ProductionId", "Title");
            return View();
        }

        //
        // POST: /Performance/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Performances.AddObject(performance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductionId = new SelectList(db.Productions, "ProductionId", "Title", performance.ProductionId);
            return View(performance);
        }

        //
        // GET: /Performance/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Performance performance = db.Performances.Single(p => p.PerformanceId == id);
            ViewBag.ProductionId = new SelectList(db.Productions, "ProductionId", "Title", performance.ProductionId);
            return View(performance);
        }

        //
        // POST: /Performance/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Performances.Attach(performance);
                db.ObjectStateManager.ChangeObjectState(performance, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductionId = new SelectList(db.Productions, "ProductionId", "Title", performance.ProductionId);
            return View(performance);
        }

        //
        // GET: /Performance/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Performance performance = db.Performances.Single(p => p.PerformanceId == id);
            return View(performance);
        }

        //
        // POST: /Performance/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Performance performance = db.Performances.Single(p => p.PerformanceId == id);
            db.Performances.DeleteObject(performance);
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