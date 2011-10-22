using System.Linq;
using System.Web.Mvc;

namespace SceneAndHeard.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Index()
        {
            var context = new SceneAndHeardDataContext("server=localhost;database=SceneCRM;Trusted_Connection=true");
            var plays = context.Plays.Select(p => p).ToList();
            return View(plays);
        }

        public ActionResult CreateView()
        {
            return View();
        }

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //public ActionResult UpdateView()
        //{
        //    return View();
        //}


        //public ActionResult Update()
        //{
        //    return View();
        //}

        //public ActionResult DeleteView()
        //{
        //    return View();
        //}

        //public ActionResult Delete()
        //{
        //    return View();
        //}
    }
}