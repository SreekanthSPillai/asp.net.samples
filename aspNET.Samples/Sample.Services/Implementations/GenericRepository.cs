using Sample.Data;
using Sample.Services.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Sample.Services.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly EmployeeDBContext context;
        private IDbSet<T> entities;
        string errorMessage = string.Empty;

        public GenericRepository(EmployeeDBContext context)
        {
            this.context = context;
        }

        public IQueryable<T> GetAll()
        {
            return Entities;
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                Entities.Add(entity);
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                var origional = context.Set<T>().Find(entity.ID);
                context.Entry(origional).CurrentValues.SetValues(entity);
                context.Entry(origional).State = EntityState.Modified;

                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                Entities.Remove(entity);
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<T>();
                }

                return entities;
            }
        }
    }
}
