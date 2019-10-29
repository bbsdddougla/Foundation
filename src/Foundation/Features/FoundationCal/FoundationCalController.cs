using System.Web.Mvc;

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
            //return "Hello World";
            //return View(new FoundationCalViewModel());
            return View(new FoundationCalViewModel());
        }
    }
}
