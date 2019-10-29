using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Foundation.Features.FoundationCal
{
    [Authorize(Roles = "Administrators, WebAdmins")]
    public class FoundationCalController : Controller
    {
        public FoundationCalController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
