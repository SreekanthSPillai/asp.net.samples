using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using DalSoft.RestClient;
using Sample.Data;
using Sample.Services.Interfaces;
using Sample.Utilities;

namespace Sample.Services.API
{
	public class FakeDataRepository<T>  where T : BaseEntity
    {
        IDataContext<T> fakeData;

        public FakeDataRepository(IDataContext<T> context)
		{
            fakeData = context;
        }

		public IQueryable<T> GetAll()
		{
            return fakeData.GetAll();
		}

		public T GetById(object id)
		{          
            return fakeData.GetById(id);
        }

        public void Insert(T entity)
        {
            fakeData.Insert(entity);
        }

        public static void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public static void Delete(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
