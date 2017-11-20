using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using System.IO;
using Nancy.Responses;

namespace clausndk.services.dnslookup
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", HomePage);
        }

        private async Task<object> HomePage(object arg)
        {
            return new GenericFileResponse("home.html", "text/html", Context);
        }
    }
}
