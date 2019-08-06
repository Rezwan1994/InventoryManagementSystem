using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Repository
{
    public class FuelBillRepository : Repository<PurchaseOrder>
    {
        DataContext context = null;
        public FuelBillRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }
        public FuelModel GetFuels(FuelFilter filter)
        {
            string searchTextQuery = "";
            string subquery = "";
            string subquery1 = "";
            string filterQuery = "";
            string CountTextQuery = "";


            if (filter.Make != new Guid())
            {
                filterQuery += string.Format(" fu.CarId = '{0}' and", filter.Make);
            }

            if (!string.IsNullOrEmpty(filter.Odometer))
            {
                filterQuery += string.Format(" fu.Odometer = '{0}' and", filter.Odometer);
            }

            if (!string.IsNullOrEmpty(filter.LastReading))
            {
                filterQuery += string.Format(" fu.LastReading = '{0}' and", filter.LastReading);
            }
            if (!string.IsNullOrEmpty(filter.VoucharNo))
            {
                filterQuery += string.Format(" fu.VoucharNo = '{0}' and", filter.VoucharNo);
            }
            if (!string.IsNullOrEmpty(filter.FuelSystem) && filter.FuelSystem != "-1")
            {
                filterQuery += string.Format(" fu.FuelSystem = '{0}' and", filter.FuelSystem);
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                searchTextQuery = " c.CarName like '%" + filter.SearchText + "%' or d.DriverName like '%" + filter.SearchText + "%' or fu.FuelSystem like '%" + filter.SearchText + "%' or fu.FuelAmount like '%" + filter.SearchText + "%' or  fu.Milage like '%" + filter.SearchText + "%' or fu.UnitPrice like '%" + filter.SearchText + "%'  and ";
                CountTextQuery = " where c.CarName like '%" + filter.SearchText + "%' or d.DriverName like '%" + filter.SearchText + "%' or fu.FuelSystem like '%" + filter.SearchText + "%' or fu.FuelAmount like '%" + filter.SearchText + "%' or  fu.Milage like '%" + filter.SearchText + "%' or fu.UnitPrice like '%" + filter.SearchText + "%'  ";
            }

            List<Product> OpportunityList = new List<Product>();

            string rawQuery = @"  
                                declare @pagesize int
                                declare @pageno int 
                                set @pagesize = " + filter.UnitPerPage + @"
                                set @pageno = " + filter.PageNumber + @"
                                declare @pagestart int
                                set @pagestart=(@pageno-1)* @pagesize  
                                select  TOP (@pagesize) fu.*,'[' + c.Make + ' ' + c.Model + '](' + c.RegId + ')' as CarName,c.Type, c.Status, d.Name as DriverName FROM FuelBills  fu

                                   left join Cars c on c.CarId = fu.CarId
                                left join Drivers d on d.DriverId = fu.DriverId

                                where {1}{2}  c.Id NOT IN(Select TOP (@pagestart) Id from Cars {0})
                                {0}
                      ";

            string CountQuery = string.Format("Select * from FuelBills c {0}", CountTextQuery);



            #region Order
            if (!string.IsNullOrWhiteSpace(filter.Order))
            {
                if (filter.Order == "ascending/carname")
                {
                    subquery = "order by c.CarName asc";

                }
                else if (filter.Order == "descending/carname")
                {
                    subquery = "order by c.CarName desc";

                }
                else if (filter.Order == "ascending/drivername")
                {
                    subquery = "order by d.DriverName asc";

                }
                else if (filter.Order == "descending/drivername")
                {
                    subquery = "order by d.DriverName desc";

                }
                else if (filter.Order == "ascending/fuelsystem")
                {
                    subquery = "order by fu.FuelSystem asc";

                }
                else if (filter.Order == "descending/fuelsystem")
                {
                    subquery = "order by fu.FuelSystem desc";

                }
                else if (filter.Order == "ascending/fuelamount")
                {
                    subquery = "order by fu.FuelAmount asc";

                }
                else if (filter.Order == "descending/fuelamount")
                {
                    subquery = "order by fu.FuelAmount  desc";

                }
                else if (filter.Order == "ascending/unitprice")
                {
                    subquery = "order by fu.UnitPrice asc";

                }
                else if (filter.Order == "descending/unitprice")
                {
                    subquery = "order by fu.UnitPrice desc";

                }
                else if (filter.Order == "ascending/issuedate")
                {
                    subquery = "order by fu.IssueDate asc";

                }
                else if (filter.Order == "descending/issuedate")
                {
                    subquery = "order by  fu.IssueDate desc";

                }

            }
            else
            {
                subquery = "order by fu.Id desc";
                subquery1 = "order by fu.Id desc";
            }
            #endregion

            rawQuery = string.Format(rawQuery, subquery, searchTextQuery, filterQuery);
            List<FuelBillVM> dsResult = context.Database.SqlQuery<FuelBillVM>(rawQuery, new object[] { }).ToList<FuelBillVM>();

            //List<EF.Person> listPerson = dbEntities.Database.SqlQuery<EF.Person_Reflect>(sql, new object[] { }).ToList<EF.Person>();

            //List<CarVM> dsResult = context.Set<CarVM>().SqlQuery(rawQuery).ToList<CarVM>();
            FuelModel FuelModel = new FuelModel();
            FuelModel.FuelList = dsResult;
            int TotalCount = context.Set<PurchaseOrder>().SqlQuery(CountQuery).ToList().Count;
            FuelModel.TotalCount = TotalCount;

            return FuelModel;


        }
        public List<PurchaseOrder> GetAllBillsbyIdList(List<string> idList)
        {
            string ids = "";
            if (idList.Count > 0)
            {
                foreach (var item in idList)
                {
                    ids += item + ",";
                }
                ids = ids.Remove(ids.Length - 1);
            }

            string rawQuery = @"  
                                select  * FROM FuelBills  
                           
                                where Id in ({0})
								
                               ";


            string sqlQuery = string.Format(rawQuery, ids);



            List<PurchaseOrder> dsResult = context.Set<PurchaseOrder>().SqlQuery(sqlQuery).ToList();


            return dsResult;


        }

        public FuelBillVM GetFuelById(int Id)
        {
            string rawQuery = @"select fu.*,dr.Name as DriverName,'[' + c.Make + ' ' + c.Model + '](' + c.RegId + ')' as CarName from FuelBills fu
                                left join Drivers dr on dr.DriverId = fu.DriverId
                                left join Cars c on c.CarId = fu.CarId
                                where fu.Id = {0}
							
                               ";
            string sqlQuery = string.Format(rawQuery, Id);
            //List<UserDriverMap> dsResult = context.Set<UserDriverMap>().SqlQuery(sqlQuery).ToList();
            FuelBillVM dsResult = context.Database.SqlQuery<FuelBillVM>(sqlQuery, new object[] { }).ToList<FuelBillVM>().FirstOrDefault();
            return dsResult;
        }
    }
}
