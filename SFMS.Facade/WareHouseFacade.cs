using IMSRepository;
using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class WareHouseFacade : Facade<WareHouse>
    {
        WareHouseRepository wareHouseRepository= null;
        public WareHouseFacade(DataContext dataContext) : base(dataContext)
        {
            wareHouseRepository = new WareHouseRepository(dataContext);
        }
        public WarehouseModel GetWareHouses(WarehouseFilter filter)
        {
            return wareHouseRepository.GetWareHouses(filter);
        }

        public List<WareHouse> GetAllWarehousesbyQuery(string query)
        {
            return wareHouseRepository.GetAllWarehousesbyQuery(query);
        }

        public WareHouse GetByWarehouseId(Guid WarehouseId)
        {
            return wareHouseRepository.GetByWarehouseId(WarehouseId);
        }
    }
}
