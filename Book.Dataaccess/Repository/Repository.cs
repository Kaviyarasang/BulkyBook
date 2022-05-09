using Book.Dataaccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dataaccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbcontext _db;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbcontext db)
        {
          _db=db;
            this.dbset=_db.Set<T>();

        }

        public void Add(T Entity)
        {
            dbset.Add(Entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbset;
            return query.ToList();
        }

        public T GetFirstorDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T Entity)
        {
            dbset.Remove(Entity);
        }

        public void Removerange(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }
    }
}
