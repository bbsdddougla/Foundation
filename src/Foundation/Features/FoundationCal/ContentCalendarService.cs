using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace Foundation.Features.FoundationCal
{
    [ServiceConfiguration(typeof(IContentCalendarService))]
    public class ContentCalendarService  : IContentCalendarService
    {
        public IEnumerable<IContent> GetContentByPublishDate(GetContentByPublishDateRequest request) => throw new System.NotImplementedException();
    }
}