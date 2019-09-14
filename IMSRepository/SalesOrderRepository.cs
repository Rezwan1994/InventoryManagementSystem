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
                                select so.*,pr.PaymentAmount as PaymentAmount,pr.BalanceDue as BalanceDue from SalesOrders so 
                                left join PaymentReceives pr on pr.SalesOrderId = so.SalesOrderId where so.SalesOrderId ='{0}'
                               ";

      

            string sqlQuery = string.Format(rawQuery, SalesOrderId);

            try
            {
                var ctx = DataContext.getInstance();
                
                SalesOrderList = ctx.Database.SqlQuery<SalesOrderVM>(sqlQuery, new object[] { }).ToList<SalesOrder>();

            }
            catch (Exception ex)
            {

            }
          
            if(SalesOrderList != null)
            {
                salesOrder = SalesOrderList.FirstOrDefault();
            }
          
            return salesOrder;
        }
    }
}
