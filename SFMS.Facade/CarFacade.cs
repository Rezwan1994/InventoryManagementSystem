using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class CarFacade : Facade<Car>
    {
        public CarFacade(DataContext dataContext) : base(dataContext) { }
    }
}
