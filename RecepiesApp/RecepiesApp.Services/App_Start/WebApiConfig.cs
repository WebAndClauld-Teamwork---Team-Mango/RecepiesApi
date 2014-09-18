using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.OData.Extensions;
using System.Web.Http.Cors;

namespace RecepiesApp.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            var attribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(attribute);
            config.AddODataQueryFilter();

            // CHEATING:
            //to cheat use "nickname=IAmCheater&sessionKey=IndeedIAm" - the system will act as if you're logged in

            // MAIN ROUTES:
            //DefaultApi + api/{controller}/all?nickname={nickname}&sessionKey={sessionKey}
            //DefaultApi + api/{controller}/select?id={id}&nickname={nickname}&sessionKey={sessionKey}
            //DefaultApi + api/{controller}/add?nickname={nickname}&sessionKey={sessionKey} (+ objectToAdd in body)
            //DefaultApi + api/{controller}/edit?id={id}&nickname={nickname}&sessionKey={sessionKey} (+ editedObject in body)
            //DefaultApi + api/{controller}/delete?id={id}&nickname={nickname}&sessionKey={sessionKey}

            // CUSTOM ROUTES:
            //DefaultApi + api/çomments/byuser?id={id}&nickname={nickname}&sessionKey={sessionKey}
            //DefaultApi + api/çomments/onrecepie?id={id}&nickname={nickname}&sessionKey={sessionKey}
            //DefaultApi + api/recepiephases/byrecepie?id={id}&nickname={nickname}&sessionKey={sessionKey}
            //DefaultApi + api/favourites/byuser?id={id}&nickname={nickname}&sessionKey={sessionKey} - returns recepies
            //DefaultApi + api/favourites/byrecepie?id={id}&nickname={nickname}&sessionKey={sessionKey} - returns users
            //DefaultApi + api/recepies/minutes?id={id}&nickname={nickname}&sessionKey={sessionKey} - returns time to prepare (int)
            //DefaultApi + api/userinfo/register(+ UserObject in body)
            //DefaultApi + api/userinfo/login(+ UserObject in body)
            //DefaultApi + api/userinfo/logout?nickname={nickname}&sessionKey={sessionKey}

            // OTHER
            //Favourites has NO "All" method!
            //Favourites has NO "Select" method!
            //Favourites has NO "Edit" method!

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
