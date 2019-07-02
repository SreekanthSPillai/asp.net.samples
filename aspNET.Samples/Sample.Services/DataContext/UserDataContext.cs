using DalSoft.RestClient;
using Sample.Data;
using Sample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Services.DataContext
{
    public class UserDataContext : IDataContext<User>
    {

        dynamic restClient = new RestClient("https://jsonplaceholder.typicode.com");
        public void Delete(User entity)
        {
            restClient.Users(entity).Delete();  
        }

        public IQueryable<User> GetAll()
        {
            var fetchTask = Task<IQueryable<User>>.Run(() => {
                return restClient.Users().Get();
            });

            return Task.FromResult(fetchTask.Result);
        }

        public User GetById(object id)
        {
            return restClient.Users.Get(1).Result;
        }

        public void Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
