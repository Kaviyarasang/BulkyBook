﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dataaccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstorDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T Entity);
        void Remove(T Entity);  
        void Removerange(IEnumerable<T> entities);
     
    }
}
