namespace RecepiesApp.Data.Repository
{
    using System;
    using System.Linq;
    using RecepiesApp.Models;

    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        void Delete(T entity);

        void Detach(T entity);

        void SaveChanges();

        void Update(T entity);
    }
}