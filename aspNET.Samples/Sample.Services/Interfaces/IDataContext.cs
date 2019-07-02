using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Data;

namespace Sample.Services.Interfaces
{
    public interface IDataContext<T> where T : BaseEntity
    {

        IList<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
