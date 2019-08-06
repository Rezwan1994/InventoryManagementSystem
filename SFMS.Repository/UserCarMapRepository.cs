using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Repository
{
    public class UserCarMapRepository : Repository<PurchaseOrderDetail>
    {

        DataContext context = null;
        public UserCarMapRepository(DataContext dataContext) : base(dataContext)
        {
            this.context = dataContext;
        }

        public PurchaseOrderDetail GetUserCarMapByDriverId(Guid CarId)
        {



            string rawQuery = @"select * from UserCarMaps
                           
                                where CarId = '{0}'
								
                               ";


            string sqlQuery = string.Format(rawQuery, CarId);
            PurchaseOrderDetail dsResult = context.Set<PurchaseOrderDetail>().SqlQuery(sqlQuery).ToList().FirstOrDefault();
          

            return dsResult;


        }

        public List<PurchaseOrderDetail> GetUserCarMapByUserId(Guid UserId)
        {



            string rawQuery = @"select * from UserCarMaps
                           
                                where UserId = '{0}'
								
                               ";


            string sqlQuery = string.Format(rawQuery, UserId);
            List<PurchaseOrderDetail> dsResult = context.Set<PurchaseOrderDetail>().SqlQuery(sqlQuery).ToList();


            return dsResult;


        }
    }
    
}
