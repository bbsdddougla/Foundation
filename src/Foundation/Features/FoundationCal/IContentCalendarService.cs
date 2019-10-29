using System.Collections.Generic;
using EPiServer.Core;

namespace Foundation.Features.FoundationCal
{
    public interface IContentCalendarService
    {
        IEnumerable<PageData> GetContentByPublishDate(GetContentByPublishDateRequest request);
    }
}