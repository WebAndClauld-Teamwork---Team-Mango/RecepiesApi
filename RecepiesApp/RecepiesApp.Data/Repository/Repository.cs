namespace RecepiesApp.Data.Repository
{
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System;

    using RecepiesApp.Data;
    using RecepiesApp.Models;

    public class Repository<T> : IRepository<T> where T : MarkableAsDeleted
    {
        public Repository()
            : this(new RecepiesDbContext())
        {
        }

        public Repository(IRecepiesDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }
        
        public IQueryable<T> All()
        {
            var all = this.set.Where(item => item.IsDeleted != true);
            return all;
        }

        public IQueryable<T> AllWithDeleted()
        {
            return this.set;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
        }

        public void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        private IRecepiesDbContext context;
        private IDbSet<T> set;

        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}