using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Repository
{
    public class ConcernsRepository : Repository<Concerns>
    {
        DataContext context = null;
        public ConcernsRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public ConcernModel GetConcerns(ConcernFilter filter)
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
                                select  TOP (@pagesize) c.* FROM Concerns c
                           
                                where {1}{2}  c.Id NOT IN(Select TOP (@pagestart) Id from Concerns {0})
                                {0}
                               ";

            string CountQuery = string.Format("Select * from Concerns c {0}", CountTextQuery);

            rawQuery = string.Format(rawQuery, subquery, searchTextQuery, filterQuery);
            int TotalCount = 0;
            List<Concerns> dsResult = new List<Concerns>();
            try
            {
                var ctx = DataContext.getInstance();
                dsResult = ctx.Concerns.SqlQuery(rawQuery).ToList();
                TotalCount = ctx.Concerns.SqlQuery(CountQuery).ToList().Count;
            }
            catch (Exception ex)
            {

            }

            ConcernModel concernModel = new ConcernModel();
            concernModel.ConcernsList = dsResult;

            //context.Dispose();
            concernModel.TotalCount = TotalCount;
            return concernModel;
        }

        public List<Concerns> GetAllConcernsbyQuery(string query)
        {
            List<Concerns> ConcernList = new List<Concerns>();

            string rawQuery = @"  
                                
                               
                                select  * FROM Concerns 
                           
                                where  (ConcernName like '%{0}%')
								
                               ";


            string sqlQuery = string.Format(rawQuery, query);



            List<Concerns> dsResult = context.Set<Concerns>().SqlQuery(sqlQuery).ToList();


            return dsResult;


        }
    }
}
