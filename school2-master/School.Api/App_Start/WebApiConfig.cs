using school.API.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace school.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.MessageHandlers.Add(new schoolDelegatingHandler());
            config.Filters.Add(new schoolActionFilter());
            config.Filters.Add(new schoolExceptionFilter());
            //config.Filters.Add(new ValidateModelAttribute());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultRout",
                routeTemplate: "wapi/{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
