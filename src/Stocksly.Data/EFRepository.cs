﻿using Stocksly.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace Stocksly.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext db;
        protected readonly DbSet<T> dbset;

        public EFRepository(DbContext dbcontext)
        {
            db = dbcontext;
            dbset = db.Set<T>();
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = db.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                dbset.Add(entity);
            }
        }

        public void Delete(int id)
        {
            T entity = Find(id);
            if (entity == null)
            {
                // HINT: Assume already deleted.
                return;
            }

            Delete(entity);
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = db.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            dbset.Attach(entity);
            dbset.Remove(entity);
        }

        public T Find(int id)
        {
            return dbset.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return dbset;
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = db.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }
    }
}
