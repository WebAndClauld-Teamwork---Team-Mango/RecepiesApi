using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;
using RecepiesApp.Services.Models;

namespace RecepiesApp.Services.Controllers
{
    public interface IController
    {
        HttpResponseMessage All();
        
        HttpResponseMessage Select(int id);

        HttpResponseMessage Delete(int id);
    }
}