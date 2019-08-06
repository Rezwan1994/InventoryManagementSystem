using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class WareHouse : Entity
    {
 
        public Guid WarehouseId { get; set; }
        public Guid WarehouseName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }

  
}




