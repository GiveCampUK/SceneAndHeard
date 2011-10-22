using System.Data;
using System.Linq;
using System.Web.Mvc;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers
{ 
    public class ProductionController : Controller
    {
        private SceneCRM db = new SceneCRM();

        //
        // GET: /Production/

        public ViewResult Index()
        {
            return View(db.Productions.ToList());
        }

        //
        // GET: /Production/Details/5

        public ViewResult Details(int id)
        {
            Production production = db.Productions.Single(p => p.ProductionId == id);
            return View(production);
        }

        //
        // GET: /Production/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Production/Create

        [HttpPost]
        public ActionResult Create(Production production)
        {
            if (ModelState.IsValid)
            {
                db.Productions.AddObject(production);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(production);
        }
        
        //
        // GET: /Production/Edit/5
 
        public ActionResult Edit(int id)
        {
            Production production = db.Productions.Single(p => p.ProductionId == id);
            return View(production);
        }

        //
        // POST: /Production/Edit/5

        [HttpPost]
        public ActionResult Edit(Production production)
        {
            if (ModelState.IsValid)
            {
                db.Productions.Attach(production);
                db.ObjectStateManager.ChangeObjectState(production, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(production);
        }

        //
        // GET: /Production/Delete/5
 
        public ActionResult Delete(int id)
        {
            Production production = db.Productions.Single(p => p.ProductionId == id);
            return View(production);
        }

        //
        // POST: /Production/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Production production = db.Productions.Single(p => p.ProductionId == id);
            db.Productions.DeleteObject(production);
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