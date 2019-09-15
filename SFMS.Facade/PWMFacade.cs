using IMSRepository;
using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class PWMFacade: Facade<ProductWarehouseMap>
    {
        PWMRepository pwmRepository = null;
        public PWMFacade(DataContext dataContext) : base(dataContext)
        {
            pwmRepository = new PWMRepository(dataContext);
        }

        public ProductWarehouseMap GetByWarehouseId(Guid WarehouseId)
        {
            //return context.Set<WareHouse>().Where(x => x.WarehouseId == WarehouseId).FirstOrDefault();
            return pwmRepository.GetByWarehouseId(WarehouseId);
        }
    }
}
