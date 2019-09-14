using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMSRepository
{
    public class PaymentRepository : Repository<PaymentReceive>
    {
        DataContext context = null;
        public PaymentRepository(DataContext dataContext) : base(dataContext)
        {
            this.context = dataContext;
        }

        public List<PaymentReceive> GetAllPaymentReceiveByCustomerId(Guid CustomerId)
        {
            List<PaymentReceive> PaymentReceiveList = new List<PaymentReceive>();

            string rawQuery = @"  
                                select *,us.Name from PaymentReceives pr 
                                left join SalesOrders so on so.SalesOrderId = pr.SalesOrderId
                                left join Users us on us.UserId = so.CustomerId
                                where us.UserId = '{0}'
                               ";

            string sqlQuery = string.Format(rawQuery, CustomerId);
            PaymentReceiveList = context.Set<PaymentReceive>().SqlQuery(sqlQuery).ToList();
            return PaymentReceiveList;
        }

        public PaymentReceive GetPaymentBySOId(Guid SalesOrderId)
        {
            return context.Set<PaymentReceive>().Where(x=>x.SalesOrderId == SalesOrderId).FirstOrDefault();
        }
    }
}
