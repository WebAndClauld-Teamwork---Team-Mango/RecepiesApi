namespace RecepiesApp.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using RecepiesApp.Data.Repository;
    using RecepiesApp.Models;

    public class RecepiesData : IRecepiesData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;
        
        public RecepiesData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public RecepiesData()
            : this(new RecepiesDbContext())
        {
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : MarkableAsDeleted
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }


        public IRepository<Recepie> Recepies
        {
            get { return this.GetRepository<Recepie>(); }
        }

        public IRepository<RecepieComment> RecepieComments
        {
            get { return this.GetRepository<RecepieComment>(); }
        }

        public IRepository<RecepiePhase> RecepiePhases
        {
            get { return this.GetRepository<RecepiePhase>(); }
        }

        public IRepository<Tag> Tags
        {
            get { return this.GetRepository<Tag>(); }
        }

        public IRepository<UserFavouriteRecepie> UserFavouriteRecepies
        {
            get { return this.GetRepository<UserFavouriteRecepie>(); }
        }

        public IRepository<UserInfo> UserInfos
        {
            get { return this.GetRepository<UserInfo>(); }
        }
    }
}
