using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int Quantity { get; set; }
    }

    [NotMapped]
    public class PWMvm : ProductWarehouseMap
    {   
        public string ProductName { get; set; }
        public double BuyingPrice { get; set; }

        public double SellingPrice { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        
        public string ImageUrl { get; set; }

        
        public string WarehouseName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}




