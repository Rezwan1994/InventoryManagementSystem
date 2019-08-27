using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMSRepository
{
    public class SalesOrderRepository : Repository<SalesOrder>
    {
        DataContext context = null;
        public SalesOrderRepository(DataContext dataContext) : base(dataContext) { }

        //public UsersModel GetInvoiceByCustomerId(Guid CustomerId)
        //{
        //    string searchTextQuery = "";
        //    string subquery = "";
        //    string filterQuery = "";
        //    string CountTextQuery = "";

        //    List<Users> OpportunityList = new List<Users>();

        //    string rawQuery = @"  
        //                        declare @pagesize int
        //                        declare @pageno int 
        //                        set @pagesize = " + filter.UnitPerPage + @"
        //                        set @pageno = " + filter.PageNumber + @"
        //                        declare @pagestart int
        //                        set @pagestart=(@pageno-1)* @pagesize  
        //                        select  TOP (@pagesize) c.* FROM Users c
                           
        //                        where {1}{2}  c.Id NOT IN(Select TOP (@pagestart) Id from Users {0})
        //                        {0}
        //                       ";

        //    string CountQuery = string.Format("Select * from Users c {0}", CountTextQuery);

        //    rawQuery = string.Format(rawQuery, subquery, searchTextQuery, filterQuery);
        //    int TotalCount = 0;
        //    List<Users> dsResult = new List<Users>();
        //    try
        //    {
        //        var ctx = DataContext.getInstance();
        //        dsResult = ctx.Users.SqlQuery(rawQuery).ToList();
        //        TotalCount = ctx.Users.SqlQuery(CountQuery).ToList().Count;
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    UsersModel usersModel = new UsersModel();
        //    usersModel.UsersList = dsResult;

        //    //context.Dispose();
        //    usersModel.TotalCount = TotalCount;
        //    return usersModel;
        //}
    }
}
