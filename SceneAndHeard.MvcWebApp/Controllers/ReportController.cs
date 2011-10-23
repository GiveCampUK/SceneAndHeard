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
        [Authorize]
        public ActionResult CRBChecks()
        {
            var expired = from vcc in context.VolunteerCrbChecks
                          where ((vcc.ApplicationExpiryDate.HasValue && vcc.ApplicationExpiryDate.Value < DateTime.UtcNow) || (vcc.Approved.HasValue && !vcc.Approved.Value))
                          select vcc;
            return View(expired);
        }

    }
}
