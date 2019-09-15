using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSRepository
{
    public class WareHouseRepository : Repository<WareHouse>
    {
        DataContext context = null;
        public WareHouseRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public WarehouseModel GetWareHouses(WarehouseFilter filter)
        {
            string searchTextQuery = "";
            string subquery = "";
            string filterQuery = "";
            string CountTextQuery = "";

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                searchTextQuery = " c.WarehouseName like '%" + filter.SearchText + "%' or c.Address like '%" + filter.SearchText + "%' and ";
                CountTextQuery = " where c.WarehouseName like '%" + filter.SearchText + "%' or c.Address like '%" + filter.SearchText + "%' and ";
            }

            string rawQuery = @"  
                                declare @pagesize int
                                declare @pageno int 
                                set @pagesize = " + filter.UnitPerPage + @"
                                set @pageno = " + filter.PageNumber + @"
                                declare @pagestart int
                                set @pagestart=(@pageno-1)* @pagesize  
                                select  TOP (@pagesize) c.* FROM WareHouses c
                           
                                where {1}{2}  c.Id NOT IN(Select TOP (@pagestart) Id from WareHouses {0})
                                {0}
                               ";

            string CountQuery = string.Format("Select * from WareHouses c {0}", CountTextQuery);

            rawQuery = string.Format(rawQuery, subquery, searchTextQuery, filterQuery);
            int TotalCount = 0;
            List<WareHouse> dsResult = new List<WareHouse>();
            try
            {
                var ctx = DataContext.getInstance();
                dsResult = ctx.WareHouse.SqlQuery(rawQuery).ToList();
                TotalCount = ctx.WareHouse.SqlQuery(CountQuery).ToList().Count;
            }
            catch (Exception ex)
            {

            }

            WarehouseModel warehouseModel = new WarehouseModel();
            warehouseModel.WareHouseList = dsResult;

            //context.Dispose();
            warehouseModel.TotalCount = TotalCount;
            return warehouseModel;
        }

        public List<WareHouse> GetAllWarehousesbyQuery(string query)
        {
            List<WareHouse> ConcernList = new List<WareHouse>();

            string rawQuery = @"  
                                select  * FROM WareHouses 
                                where  (WarehouseName like '%{0}%')
                               ";

            string sqlQuery = string.Format(rawQuery, query);
            List<WareHouse> dsResult = context.Set<WareHouse>().SqlQuery(sqlQuery).ToList();
            return dsResult;
        }

        public WareHouse GetByWarehouseId(Guid WarehouseId)
        {
            return context.Set<WareHouse>().Where(x => x.WarehouseId == WarehouseId).FirstOrDefault();
        }
    }
}
