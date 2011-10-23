using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SceneCrm.Entities;

namespace SceneAndHeard.Controllers
{
    [Authorize(Roles = "Editor")]
    public class ReportController : Controller
    {
        private SceneCRM context = new SceneCRM();

        //
        // GET: /Report/
        [Authorize]
        public ActionResult CRBChecks()
        {
            return View(context.VolunteerCrbChecks);
        }

    }
}
