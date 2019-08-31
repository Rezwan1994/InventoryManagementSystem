using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMSRepository
{
    public class SalesOrderRepository : Repository<SalesOrder>
    {
        DataContext context = null;
        public SalesOrderRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public SalesOrder GetSalesOrderBySalesOrderId(Guid SalesOrderId)
        {
            List<SalesOrder> SalesOrderList = new List<SalesOrder>();
            SalesOrder salesOrder = new SalesOrder();
            string rawQuery = @"  
                                select  * FROM SalesOrders 
                                where  SalesOrderId = '{0}'
                               ";

      

            string sqlQuery = string.Format(rawQuery, SalesOrderId);
            SalesOrderList = context.Set<SalesOrder>().SqlQuery(sqlQuery).ToList();
            if(SalesOrderList != null)
            {
                salesOrder = SalesOrderList.FirstOrDefault();
            }
          
            return salesOrder;
        }
    }
}
