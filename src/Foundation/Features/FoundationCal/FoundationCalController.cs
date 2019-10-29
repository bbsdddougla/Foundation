using System;
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
            request.Take = 20;
            request.BeginDate = new DateTime(2019, 10, 1);
            request.EndDate = new DateTime(2019, 10, 31);
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
                start = content.StartPublish.Value.ToString("s"),
                EndDate = content.StartPublish,
                title = content.Name
            };
        }
    }
}
