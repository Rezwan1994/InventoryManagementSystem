using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSRepository
{
    public class PWMRepository : Repository<ProductWarehouseMap>
    {
        DataContext context = null;
        public PWMRepository(DataContext dataContext) : base(dataContext)
        {
            this.context = dataContext;
        }
    }
}
