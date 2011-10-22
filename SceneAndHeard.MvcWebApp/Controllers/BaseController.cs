using System.Linq;
using System.Web.Mvc;

namespace SceneAndHeard.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Index()
        {
         
            return View();
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