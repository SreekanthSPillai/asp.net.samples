using System.Linq;
using Sample.Data;

namespace Sample.Services.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
