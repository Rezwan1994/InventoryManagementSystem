using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace IMSRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : SFMS.Entity.Entity
    {
        DataContext context = null;
        public Repository(DataContext dataContext)
        {
           this.context = dataContext;
        }

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public int Insert(TEntity entity)
        {
            int res = 0;
            try
            {
                context.Set<TEntity>().Add(entity);
                res = context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return res;
        }

        public int Update(TEntity entity)
        {
            try
            {
                context.Entry<TEntity>(entity).State = EntityState.Modified;
                //context.Set<TEntity>().AddOrUpdate(entity);

                return context.SaveChanges();
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            TEntity entity = Get(id);
            context.Set<TEntity>().Remove(entity);
            return context.SaveChanges();
        }
    }
}
