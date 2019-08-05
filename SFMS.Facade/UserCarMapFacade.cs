using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class UserCarMapFacade : Facade<UserCarMap>
    {
        UserCarMapRepository carMapRepo = null;
        public UserCarMapFacade(DataContext dataContext) : base(dataContext) {

            carMapRepo = new UserCarMapRepository(dataContext);
        }

        public UserCarMap GetUserCarMapByCarId(Guid CarId)
        {

            return carMapRepo.GetUserCarMapByDriverId(CarId);
        }
        public List<UserCarMap> GetUserCarMapByUserId(Guid UserId)
        {

            return carMapRepo.GetUserCarMapByUserId(UserId);
        }

    }
}
