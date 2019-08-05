using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Repository
{
    public class UserCarMapRepository : Repository<UserCarMap>
    {

        DataContext context = null;
        public UserCarMapRepository(DataContext dataContext) : base(dataContext)
        {
            this.context = dataContext;
        }

        public UserCarMap GetUserCarMapByDriverId(Guid CarId)
        {



            string rawQuery = @"select * from UserCarMaps
                           
                                where CarId = '{0}'
								
                               ";


            string sqlQuery = string.Format(rawQuery, CarId);
            UserCarMap dsResult = context.Set<UserCarMap>().SqlQuery(sqlQuery).ToList().FirstOrDefault();
          

            return dsResult;


        }

        public List<UserCarMap> GetUserCarMapByUserId(Guid UserId)
        {



            string rawQuery = @"select * from UserCarMaps
                           
                                where UserId = '{0}'
								
                               ";


            string sqlQuery = string.Format(rawQuery, UserId);
            List<UserCarMap> dsResult = context.Set<UserCarMap>().SqlQuery(sqlQuery).ToList();


            return dsResult;


        }
    }
    
}
