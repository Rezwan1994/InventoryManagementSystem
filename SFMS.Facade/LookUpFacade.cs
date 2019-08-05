﻿using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class LookUpFacade : Facade<LookUp>
    {
        LookUpRepository lookupRepo = null;
        public LookUpFacade(DataContext dataContext) : base(dataContext) {
            lookupRepo = new LookUpRepository(dataContext);
        }
        public List<LookUp> GetLookupByKey(string Datakey)
        {
            return lookupRepo.GetLookupByKey(Datakey);
        }
    }
}
