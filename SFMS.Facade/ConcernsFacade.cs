using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class ConcernsFacade : Facade<Concerns>
    {
        ConcernsRepository concernRepo = null;
        public ConcernsFacade(DataContext dataContext) : base(dataContext) {
            concernRepo = new ConcernsRepository(dataContext);
        }

        public ConcernModel GetConcerns(ConcernFilter filter)
        {
            return concernRepo.GetConcerns(filter);
        }
        public List<Concerns> GetAllConcernsbyQuery(string query)
        {

            return concernRepo.GetAllConcernsbyQuery(query);
        }
    }
}
