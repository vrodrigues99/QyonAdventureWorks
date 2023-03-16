using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructure.Data.Context;
using Domain.Entities.Base;
using Domain.Interfaces.Service;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Infrastructure.Data.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected DataContext Db;
        protected DbSet<TEntity> Model;

        public BaseRepository(DataContext context)
        {
            Db = context;
            Model = Db.Set<TEntity>();
        }

        public TEntity Add(TEntity obj)
        {
            Db.Add(obj);

            return obj;
        }

        public TEntity Update(TEntity obj)
        {
            Db.Update(obj);

            return obj;
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return Model.AsNoTracking().Where(predicate);
        }

        public TEntity GetById(int Id)
        {
            return Model.Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Model.ToList();
        }

        public void Remove(int id)
        {
            Model.Remove(GetById(id));
        }

        public void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            var items = Search(predicate);
            if (items != null && items.Count() > 0)
                foreach (var item in items)
                    Model.Remove(item);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
