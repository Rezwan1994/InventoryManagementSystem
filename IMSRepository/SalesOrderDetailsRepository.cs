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

        public List<SalesOrderDetail> GetAllSalesDetailsBySaleOrderId(Guid SalesOrderId)
        {
            List<SalesOrderDetail> ConcernList = new List<SalesOrderDetail>();

            string rawQuery = @"  
                                select  so.*,p.ProductName as ProductName FROM SalesOrderdetails so
                                left join Products p on p.ProductId = so.ProductId
                                where  so.SalesOrderId = '{0}'
                               ";

            string sqlQuery = string.Format(rawQuery, SalesOrderId);
            List<SalesOrderDetailVM> dsResult1 = context.Database.SqlQuery<SalesOrderDetailVM>(rawQuery, new object[] { }).ToList<SalesOrderDetailVM>();

            List<SalesOrderDetail> dsResult = context.Database.SqlQuery<SalesOrderDetailVM>(rawQuery, new object[] { }).ToList<SalesOrderDetail>();
            return dsResult;
        }
    }
}
