using IMSRepository;
using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class SalesOrderFacade : Facade<SalesOrder>
    {
       SalesOrderRepository salesorderRepository = null;
        public SalesOrderFacade(DataContext dataContext) : base(dataContext)
        {
            salesorderRepository = new SalesOrderRepository(dataContext);
        }
        public SalesOrder GetSalesOrderBySalesOrderId(Guid SalesOrderId)
        {
            return salesorderRepository.GetSalesOrderBySalesOrderId(SalesOrderId);
        }

        public SalesOrder GetSalesOrderByCustomerId(Guid CustomerId)
        {
            return salesorderRepository.GetSalesOrderBySalesOrderId(CustomerId);
        }
    }
}
