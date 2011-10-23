using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SceneAndHeard.Controllers
{
    [Authorize(Roles = "Editor")]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Scene & Heard Database";

            return View();
        }

        //public ActionResult About()
        //{
        //    return View();
        //}
    }
}
