using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Headers;

namespace RecepiesApp.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // MAIN ROUTES:
            //DefaultApi + api/{controller}/all
            //DefaultApi + api/{controller}/select/{id}
            //DefaultApi + api/{controller}/add (+ objectToAdd in body)
            //DefaultApi + api/{controller}/edit/{id} (+ editedObject in body)
            //DefaultApi + api/{controller}/delete/{id}

            // CUSTOM ROUTES:
            //DefaultApi + api/çomments/byuser/{id}
            //DefaultApi + api/çomments/onrecepie/{id}

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
