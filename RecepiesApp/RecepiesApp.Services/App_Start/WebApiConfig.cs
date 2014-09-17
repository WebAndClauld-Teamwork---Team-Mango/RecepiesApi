﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

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

            // MAIN ROUTES:
            //DefaultApi + api/{controller}/all
            //DefaultApi + api/{controller}/select/{id}
            //DefaultApi + api/{controller}/add (+ objectToAdd in body)
            //DefaultApi + api/{controller}/edit/{id} (+ editedObject in body)
            //DefaultApi + api/{controller}/delete/{id}

            // CUSTOM ROUTES:
            //DefaultApi + api/çomments/byuser/{id}
            //DefaultApi + api/çomments/onrecepie/{id}
            //DefaultApi + api/recepiephases/byrecepie/{id}
            //DefaultApi + api/favourites/byuser/{id} - returns recepies
            //DefaultApi + api/favourites/byrecepie/{id} - returns users

            // OTHER
            //Favourites has NO "All" method!
            //Favourites has NO "Select" method!
            //Favourites has NO "Edit" method!

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
