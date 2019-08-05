using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class UserDriverMapFacade : Facade<UserDriverMap>
    {
        UserDriverMapRepository driverMapRepo = null;
        public UserDriverMapFacade(DataContext dataContext) : base(dataContext)
        {
            driverMapRepo = new UserDriverMapRepository(dataContext);
        }
        public List<UserDriverMapVM> GetDriverMapByCarId(Guid CarId)
        {
            return driverMapRepo.GetDriverMapByCarId(CarId);
        }
        public UserDriverMap GetDriverMapById(int id)
        {
            return driverMapRepo.GetDriverMapById(id);
        }

    }

 
}
