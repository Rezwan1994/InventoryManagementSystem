using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Repository
{
    public class DriverRepository : Repository<SalesOrder>
    {
        public DriverRepository(DataContext dataContext) : base(dataContext) { }

        public DriversModel GetDrivers(DriversFilter filter)
        {
            string searchTextQuery = "";
            string subquery = "";
            string filterQuery = "";
            string CountTextQuery = "";

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                searchTextQuery = " c.Name like '%" + filter.SearchText + "%' or c.MobileNumber like '%" + filter.SearchText + "%' or c.Email like '%" + filter.SearchText + "%' or c.DriverLicense like '%" + filter.SearchText + "%' or c.Address like '%" + filter.SearchText + "%' and ";
                CountTextQuery = " where c.Name like '%" + filter.SearchText + "%' or c.MobileNumber like '%" + filter.SearchText + "%' or c.Email like '%" + filter.SearchText + "%' or c.DriverLicense like '%" + filter.SearchText + "%' or c.Address like '%" + filter.SearchText + "%' and ";
            }

            string rawQuery = @"  
                                declare @pagesize int
                                declare @pageno int 
                                set @pagesize = " + filter.UnitPerPage + @"
                                set @pageno = " + filter.PageNumber + @"
                                declare @pagestart int
                                set @pagestart=(@pageno-1)* @pagesize  
                                select  TOP (@pagesize) c.* FROM Drivers c
                           
                                where {1}{2}  c.Id NOT IN(Select TOP (@pagestart) Id from Drivers {0})
                                {0}
                               ";

            string CountQuery = string.Format("Select * from Drivers c {0}", CountTextQuery);

            rawQuery = string.Format(rawQuery, subquery, searchTextQuery, filterQuery);
            int TotalCount = 0;
            List<SalesOrder> dsResult = new List<SalesOrder>();
            try
            {
                var ctx = DataContext.getInstance();
                dsResult = ctx.Drivers.SqlQuery(rawQuery).ToList();
                TotalCount = ctx.Drivers.SqlQuery(CountQuery).ToList().Count;
            }
            catch (Exception ex)
            {

            }

            DriversModel driversModel = new DriversModel();
            driversModel.DriversList = dsResult;

            //context.Dispose();
            driversModel.TotalCount = TotalCount;
            return driversModel;
        }
    }
}
