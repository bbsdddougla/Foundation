using System;

namespace Foundation.Features.FoundationCal
{
    public class FoundationCalViewModel
    {
        public CalendarContentItem[] CalendarContentItems { get; set; }
    }

    public class CalendarContentItem
    {
        public string title { get; set; }

        public string start { get; set; }

        public DateTime? EndDate { get; set; }

        public string Color { get; set; }

        public string FaIcon { get; set; }
    }
}
