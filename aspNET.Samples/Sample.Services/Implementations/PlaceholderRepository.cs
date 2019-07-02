using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Sample.Services.Interfaces;
using Sample.Utilities;

namespace Sample.Services.Implementations
{
	public class PlaceholderRepository<T> :  IPlaceholderRepository<T> where T : class
	{
        private APIHelper dataHelper = new APIHelper();

        private string BaseUrl { get; set; }



		public PlaceholderRepository(string url)
		{
            BaseUrl = url;
		}

		public IQueryable<T> GetAll()
		{

            var data = Task<T>.Run(() => {
               
                return APIHelper.BasicCallAsync<T>(BaseUrl);
            });

            return data.Result.AsQueryable();
		}

		public T GetById(object id)
		{
            //fetch('https://jsonplaceholder.typicode.com/posts/1')
            string getByIdUrl = string.Format("{0}/{1}", BaseUrl, id);
            
            var data = Task<T>.Run(() => {
                return APIHelper.BasicCallAsync<T>(getByIdUrl);
            });

            return data.Result[0];
        }

        public Guid Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
