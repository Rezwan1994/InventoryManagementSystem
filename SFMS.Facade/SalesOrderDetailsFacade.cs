using IMSRepository;
using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class SalesOrderDetailsFacade : Facade<SalesOrderDetail>
    {
        SalesOrderDetailsRepository salesorderRepository = null;
        public SalesOrderDetailsFacade(DataContext dataContext) : base(dataContext)
        {
            salesorderRepository = new SalesOrderDetailsRepository(dataContext);
        }
        public List<SalesOrderDetailVM> GetAllSalesDetailsBySaleOrderId(Guid SalesOrderId)
        {
            return salesorderRepository.GetAllSalesDetailsBySaleOrderId(SalesOrderId);
        }

    }
}