using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class DriverFacade : Facade<Drivers>
    {
        DriverRepository driverRepository = null;
        public DriverFacade(DataContext dataContext) : base(dataContext) {
            driverRepository = new DriverRepository(dataContext);
        }

        public DriversModel GetDrivers(DriversFilter filter)
        {
            return driverRepository.GetDrivers(filter);
        }
    }
}
