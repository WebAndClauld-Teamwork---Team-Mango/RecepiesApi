using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;

namespace RecepiesApp.Data
{
    public interface IRecepiesData
    {
        IRepository<Recepie> Recepies { get; }

        IRepository<RecepieComment> RecepieComments { get; }

        IRepository<RecepiePhase> RecepiePhases { get; }

        IRepository<Tag> Tags { get; }

        IRepository<UserFavouriteRecepie> UserFavouriteRecepies { get; }

        IRepository<UserInfo> UserInfos { get;}

        int SaveChanges();
    }
}
