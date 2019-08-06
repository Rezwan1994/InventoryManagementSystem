using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity.Entity
    {
        List<TEntity> GetAll();
        TEntity Get(int id);
        int Insert(TEntity entity);
        int Update(TEntity entity);
        int Delete(int id);
    }
}