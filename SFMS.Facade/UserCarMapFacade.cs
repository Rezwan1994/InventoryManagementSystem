using SFMS.Entity;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class UserCarMapFacade : Facade<PurchaseOrderDetail>
    {
        UserCarMapRepository carMapRepo = null;
        public UserCarMapFacade(DataContext dataContext) : base(dataContext) {

            carMapRepo = new UserCarMapRepository(dataContext);
        }

        public PurchaseOrderDetail GetUserCarMapByCarId(Guid CarId)
        {

            return carMapRepo.GetUserCarMapByDriverId(CarId);
        }
        public List<PurchaseOrderDetail> GetUserCarMapByUserId(Guid UserId)
        {

            return carMapRepo.GetUserCarMapByUserId(UserId);
        }

    }
}
