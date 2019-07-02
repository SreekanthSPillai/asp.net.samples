using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Services.Interfaces
{
    public interface IPlaceholderRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        Guid Insert(T entity);

        void Update(T entity);
        void Delete(T entity);
    }
}
