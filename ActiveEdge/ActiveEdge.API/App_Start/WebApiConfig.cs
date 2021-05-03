using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiThrottle;

namespace ActiveEdge.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new TokenValidationHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                Policy = new ThrottlePolicy(perHour: 500)
                {
                    IpThrottling = true,
                    EndpointRules = new Dictionary<string, RateLimits>
                    {
                        { "api/Employee", new RateLimits { PerSecond = 1} },
                        { "api/Auth", new RateLimits { PerSecond = 1 } }
                    }
                },
                Repository = new CacheRepository()
            });
        }
    }
}
