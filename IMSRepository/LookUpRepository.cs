using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSRepository
{
  
    public class LookUpRepository : Repository<LookUp>
    {
        DataContext context = null;
        public LookUpRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public List<LookUp> GetLookupByKey(string DataKey)
        {


            context = DataContext.getInstance();
            string rawQuery = @"select * from LookUps
                           
                                where DataKey = '{0}'
								
                               ";


            string sqlQuery = string.Format(rawQuery, DataKey);
            List<LookUp> dsResult = context.Set<LookUp>().SqlQuery(sqlQuery).ToList();
            //Car dsResult = context.Database.SqlQuery<CarVM>(sqlQuery, new object[] { }).ToList<Car>().FirstOrDefault();

            return dsResult;


        }
    }


  
}
