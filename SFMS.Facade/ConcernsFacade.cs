using SFMS.Entity;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class ConcernsFacade : Facade<PaymentReceive>
    {
        ConcernsRepository concernRepo = null;
        public ConcernsFacade(DataContext dataContext) : base(dataContext) {
            concernRepo = new ConcernsRepository(dataContext);
        }

        public ConcernModel GetConcerns(ConcernFilter filter)
        {
            return concernRepo.GetConcerns(filter);
        }
        public List<PaymentReceive> GetAllConcernsbyQuery(string query)
        {

            return concernRepo.GetAllConcernsbyQuery(query);
        }
    }
}
