using Stocksly.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
