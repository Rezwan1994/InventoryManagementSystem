using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace IMS.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity.Entity
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
            context.Set<TEntity>().Add(entity);
            return context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            //context.Entry<TEntity>(entity).State = EntityState.Modified;
            context.Set<TEntity>().AddOrUpdate(entity);
          
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            TEntity entity = Get(id);
            context.Set<TEntity>().Remove(entity);
            return context.SaveChanges();
        }
    }
}
