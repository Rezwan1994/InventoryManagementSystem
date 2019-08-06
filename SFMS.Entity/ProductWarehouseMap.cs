using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity 
{
    public class ProductWarehouseMap : Entity
    {
        public Guid ProductWarehouseMapId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid ProductId { get; set; }
        public string Quantity { get; set; }
  
    }
}




