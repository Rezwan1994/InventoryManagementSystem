using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMSRepository
{
    public class SalesOrderDetailsRepository : Repository<SalesOrderDetail>
    {
        DataContext context = null;
        public SalesOrderDetailsRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public List<SalesOrderDetailVM> GetAllSalesDetailsBySaleOrderId(Guid SalesOrderId)
        {
            List<SalesOrderDetailVM> dsResult = new List<SalesOrderDetailVM>();

            string rawQuery = @"  
                                select  so.*,p.ProductName as ProductName, w.WarehouseName FROM SalesOrderdetails so
                                left join Products p on p.ProductId = so.ProductId
                                left join WareHouses w on w.WarehouseId = so.WarehouseId
                                where  so.SalesOrderId = CONVERT(uniqueidentifier, '{0}')
                                ";

            string sqlQuery = string.Format(rawQuery, SalesOrderId);
            //List<SalesOrderDetailVM> dsResult = context.Database.SqlQuery<SalesOrderDetailVM>(rawQuery, new object[] { }).ToList<SalesOrderDetailVM>();
            try
            {
                var ctx = DataContext.getInstance();
                dsResult = ctx.Database.SqlQuery<SalesOrderDetailVM>(sqlQuery, new object[] { }).ToList();

            }
            catch (Exception ex) {

            }
            return dsResult;
        }
    }
}
