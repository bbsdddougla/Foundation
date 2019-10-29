using System.Linq;
using System.Web.Mvc;
using EPiServer.Core;

namespace Foundation.Features.FoundationCal
{
    [Authorize(Roles = "Administrators, WebAdmins")]
    public class FoundationCalController : Controller
    {
        private IContentCalendarService _contentCalendarService;

        public FoundationCalController(IContentCalendarService contentCalendarService)
        {
            _contentCalendarService = contentCalendarService;
        }

        public ActionResult Index(GetContentByPublishDateRequest request)
        {
            //return "Hello World";
            //return View(new FoundationCalViewModel());
            return View(new FoundationCalViewModel
            {
                CalendarContentItems = _contentCalendarService.GetContentByPublishDate(request).Select(x => ToCalendarContentItem(x)).ToArray()
            });
        }

        private CalendarContentItem ToCalendarContentItem(PageData content)
        {
            return new CalendarContentItem
            {
                StartDate = content.StartPublish,
                EndDate = content.StartPublish,
                Title = content.Name
            };
        }
    }
}
