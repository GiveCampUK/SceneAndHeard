using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneAndHeard.Controllers
{
    public class PlayController : BaseController
    {

    }

    public class BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateView()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult UpdateView()
        {
            return View();
        }


        public ActionResult Update()
        {
            return View();
        }

        public ActionResult DeleteView()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}
