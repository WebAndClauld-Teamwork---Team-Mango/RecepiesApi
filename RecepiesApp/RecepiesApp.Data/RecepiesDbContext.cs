using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecepiesApp.Data.Migrations;
using RecepiesApp.Models;

namespace RecepiesApp.Data
{
    public class RecepiesDbContext : DbContext, IRecepiesDbContext
    {
        public RecepiesDbContext()
            : this("Server=.;Database=RecepiesAppDatabase;Trusted_Connection=True;")
        {
        }

        public static RecepiesDbContext MsSqlExpressInstance()
        {
            return new RecepiesDbContext(@"Server=.\SQLEXPRESS;Database=RecepiesAppDatabase;Trusted_Connection=True;");
        }

        public RecepiesDbContext(string connectionString)
            :base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RecepiesDbContext,Configuration>());
        }
        
        public virtual DbSet<Recepie> Recepies { get; set; }

        public virtual DbSet<RecepieComment> RecepieComments { get; set; }

        public virtual DbSet<RecepiePhase> RecepiePhases { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<UserFavouriteRecepie> UserFavouriteRecepies { get; set; }

        public virtual DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // deliting user wont delete recepies
            modelBuilder.Entity<UserInfo>()
                .HasMany(p => p.Recepies)
                .WithRequired()
                .HasForeignKey(c => c.UserInfoId)
                .WillCascadeOnDelete(false);

            // deliting user wont delete comments
            modelBuilder.Entity<UserInfo>()
                .HasMany(p => p.RecepieComments)
                .WithRequired()
                .HasForeignKey(c => c.UserInfoId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        DbSet<T> Set<T>() where T : class 
        {
            return base.Set<T>();
        }

        DbEntityEntry<T> Entry<T>(T entity) where T : class
        {
            return base.Entry<T>(entity);
        }
    }
}
