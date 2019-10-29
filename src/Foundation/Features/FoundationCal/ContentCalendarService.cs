using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.ServiceLocation;

namespace Foundation.Features.FoundationCal
{
    [ServiceConfiguration(typeof(IContentCalendarService))]
    public class ContentCalendarService  : IContentCalendarService
    {
        private readonly IClient findClient;

        public ContentCalendarService(IClient findClient)
        {
            this.findClient = findClient;
        }

        public IEnumerable<IContent> GetContentByPublishDate(GetContentByPublishDateRequest request)
        {
            var search = findClient.Search<PageData>();

            if (request.BeginDate == null && request.EndDate == null)
            {
                search = search.Filter(p => !p.StartPublish.Exists());
            }
            else
            {
                search = search.Filter(p => p.StartPublish.After(request.BeginDate.Value) & p.StartPublish.Before(request.EndDate.Value));
            }

            return search.GetContentResult();
        }
    }
}