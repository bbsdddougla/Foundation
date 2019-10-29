using System;
using System.Collections.Generic;

namespace Foundation.Features.FoundationCal
{
    public class GetContentByPublishDateRequest
    {
        public DateTime? BeginDate {get;set;}

        public DateTime? EndDate {get;set;}

        public IEnumerable<Type> ContentTypes {get;set;}

        public string FetchPublished {get;set;}

    }
}