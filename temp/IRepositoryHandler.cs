using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;

namespace RecepiesApp.Services.Controllers
{
    public interface IRepositoryHandler<T> where T : MarkableAsDeleted
    {
        IRepository<T> Repository { get; }
    }
}