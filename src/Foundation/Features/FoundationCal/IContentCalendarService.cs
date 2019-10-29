using System.Collections.Generic;
using EPiServer.Core;

namespace Foundation.Features.FoundationCal
{
    public interface IContentCalendarService
    {
        IEnumerable<IContent> GetContentByPublishDate(GetContentByPublishDateRequest request);
    }
}