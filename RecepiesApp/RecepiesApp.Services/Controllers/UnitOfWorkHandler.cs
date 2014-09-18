using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecepiesApp.Data;

namespace RecepiesApp.Services.Controllers
{
    public static class UnitOfWorkHandler
    {
        public static IRecepiesData Data = new RecepiesData();
    }
}