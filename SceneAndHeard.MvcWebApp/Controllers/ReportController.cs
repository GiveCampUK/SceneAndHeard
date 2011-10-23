using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers
{
    public class ReportController : Controller
    {
        private SceneCRM context = new SceneCRM();

        //
        // GET: /Report/

        public ActionResult CRBChecks()
        {
            return View(context.VolunteerCrbChecks);
        }

    }
}
