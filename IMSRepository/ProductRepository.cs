using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSRepository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(DataContext dataContext) : base(dataContext) { }
    }
}
