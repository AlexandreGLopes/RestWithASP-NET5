using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWithASPNET5.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Hypermedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
            var path = "api/person/v1";
            string link = GetLink(content.Id, urlHelper, path);
        }

        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {

            }
        }
    }
}
