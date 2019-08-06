using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class Facade<TEntity> : IFacade<TEntity> where TEntity : Entity.Entity
    {
        IRepository<TEntity> repo = null;
        CarRepository carRepo = null;
        public Facade(DataContext context)
        {
            repo = new Repository<TEntity>(context);
            carRepo = new CarRepository(context);

        }

        public List<TEntity> GetAll()
        {
            return repo.GetAll();
        }
        public VehicleModel GetVehicles(VehicleFilter filter)
        {

            return carRepo.GetVehicles(filter);
        }
        public List<Product> GetAllVehiclesbyIdList(List<string> IdList)
        {

            return carRepo.GetAllCarsbyIdList(IdList);
        }
        public List<SalesOrder> GetAllAllocationsbyQuery(string query)
        {

            return carRepo.GetAllAllocationsbyQuery(query);
        }
       
        public List<Product> GetAllCarsbyQuery(string query)
        {

            return carRepo.GetAllCarsbyQuery(query);
        }
        public List<Users> GetAllOwnersbyQuery(string query)
        {

            return carRepo.GetAllOwnersbyQuery(query);
        }
        public TEntity Get(int id)
        {
            return repo.Get(id);
        }

        public Product GetVehicleById(int id)
        {
            return carRepo.GetVehicleById(id);
        }
        public Product GetVehicleByCarId(Guid CarId)
        {
            return carRepo.GetVehicleByCarId(CarId);
        }

        public int Insert(TEntity entity)
        {
            return repo.Insert(entity);
        }

        public int Update(TEntity entity)
        {
            return repo.Update(entity);
        }

        public int Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
