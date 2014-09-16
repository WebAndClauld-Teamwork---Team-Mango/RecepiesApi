using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using RecepiesApp.Data.Migrations;
using RecepiesApp.Models;

namespace RecepiesApp.Data
{
    public interface IRecepiesDbContext
    {
        DbSet<Recepie> Recepies { get; set; }

        DbSet<RecepieComment> RecepieComments { get; set; }

        DbSet<RecepiePhase> RecepiePhases { get; set; }

        DbSet<Tag> Tags { get; set; }

        DbSet<UserFavouriteRecepie> UserFavouriteRecepies { get; set; }

        DbSet<UserInfo> UserInfos { get; set; }

        void SaveChanges();

        DbSet<T> Set<T>() where T : class;
        
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}