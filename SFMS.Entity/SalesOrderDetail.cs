using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class SalesOrderDetail : Entity
    {
        public Guid SalesOrderDetailId { get; set; }
        public Guid SalesOrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
  
        public double SubTotal { get; set; }

        public double Total { get; set; }
        public Guid WarehouseId { get; set; }
    }
    [NotMapped]
    public class SalesOrderDetailVM : SalesOrderDetail
    {
        public string ProductName { get; set; }
    }
}












