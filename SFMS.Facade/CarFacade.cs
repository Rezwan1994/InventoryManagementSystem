﻿using SFMS.Entity;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class CarFacade : Facade<Product>
    {
        public CarFacade(DataContext dataContext) : base(dataContext) { }
    }
}
