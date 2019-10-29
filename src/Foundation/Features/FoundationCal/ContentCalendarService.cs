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

            if (request.BeginDate is null)
            {
                request.BeginDate = DateTime.Today;
            }

            if (request.EndDate is null)
            {
                request.EndDate = DateTime.Today.AddMonths(1);
                request.EndDate = new DateTime(request.EndDate.Value.Year, request.EndDate.Value.Month, 1);
            }


            var search = findClient.Search<PageData>();
            search = search.Filter(p => p.StartPublish.After(request.BeginDate.Value));
            search = search.Filter(p => p.StartPublish.Before(request.BeginDate.Value));
            return search.GetContentResult();
        }
    }
}