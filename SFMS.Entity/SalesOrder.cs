using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
   public class SalesOrder : Entity
    {
        public Guid SalesOrderId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid CustomerId { get; set; }

        public string Remarks { get; set; }
        public double Amount { get; set; }
        public double SubTotal { get; set; }
        public double Freight { get; set; }
        public double DiscountAmount { get; set; }

        public double TaxAmount { get; set; }
        public double Total { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DelivaryDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        [NotMapped]
        public string PaymentNote { get; set; }

        [NotMapped]
        public DateTime? PaymentDate { get; set; }
        [NotMapped]
        public double PaymentAmount { get; set; }
    }

    [NotMapped]
    public class SalesOrderModel
    {
        [NotMapped]
        public List<SalesOrderDetailVM> SalesOrderDetailList { get; set; }
        [NotMapped]
        public List<SalesOrderDetail> SalesOrderDetails { get; set; }
        [NotMapped]
        public SalesOrder SalesOrder { get; set; }

    }
}











