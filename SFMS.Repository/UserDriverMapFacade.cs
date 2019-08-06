using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Repository
{
    public class UserDriverMapRepository : Repository<WareHouse>
    {

        DataContext context = null;
        public UserDriverMapRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public List<UserDriverMapVM> GetDriverMapByCarId(Guid CarId)
        {

        

            string rawQuery = @"select um.*,dr.Name as DriverName,dr.MobileNumber as Mobile  from UserDriverMaps um
                                left join Drivers dr on dr.DriverId = um.DriverId
                                where um.CarId = '{0}'
							
                               ";


            string sqlQuery = string.Format(rawQuery, CarId);
            //List<UserDriverMap> dsResult = context.Set<UserDriverMap>().SqlQuery(sqlQuery).ToList();
            List<UserDriverMapVM> dsResult = context.Database.SqlQuery<UserDriverMapVM>(sqlQuery, new object[] { }).ToList<UserDriverMapVM>();

            return dsResult;


        }

        public WareHouse GetDriverMapById(int Id)
        {



            string rawQuery = @"select um.*,dr.Name as DriverName from UserDriverMaps um
                                left join Drivers dr on dr.DriverId = um.DriverId
                                where um.Id = {0}
							
                               ";


            string sqlQuery = string.Format(rawQuery, Id);
            //List<UserDriverMap> dsResult = context.Set<UserDriverMap>().SqlQuery(sqlQuery).ToList();
            WareHouse dsResult = context.Database.SqlQuery<UserDriverMapVM>(sqlQuery, new object[] { }).ToList<WareHouse>().FirstOrDefault();

            return dsResult;


        }
    }
}
