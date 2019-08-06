using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class PaymentReceive : Entity
    {
        public Guid PaymentId { get; set; }
        public Guid SalesOrderId { get; set; }
        public string PaymentStatus { get; set; }
        public string Note { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public double PaymentAmount { get; set; }
        public double BalanceDue { get; set; }
 

    
    }
    
  


}
