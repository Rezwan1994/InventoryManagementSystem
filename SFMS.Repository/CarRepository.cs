using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Repository
{
    public class CarRepository : Repository<Car>
    {
        DataContext context = null;
        public CarRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }


        public VehicleModel GetVehicles(VehicleFilter filter)
        {
            string searchTextQuery = "";
            string subquery = "";
            string subquery1 = "";
            string filterQuery = "";
            string CountTextQuery = "";
         
            if (!string.IsNullOrEmpty(filter.VehicleType) && filter.VehicleType != "-1")
            {
                filterQuery += string.Format(" c.Type = '{0}' and", filter.VehicleType);
            }

            if (!string.IsNullOrEmpty(filter.VehicleSubType) && filter.VehicleSubType != "-1")
            {
                filterQuery += string.Format(" c.SubType = '{0}' and", filter.VehicleSubType);
            }

            if (!string.IsNullOrEmpty(filter.Capacity) && filter.Capacity != "-1")
            {
                filterQuery += string.Format(" c.Capacity = '{0}' and", filter.Capacity);
            }

            if (!string.IsNullOrEmpty(filter.Status) && filter.Status != "-1")
            {
                filterQuery += string.Format(" c.Status = '{0}' and", filter.Status);
            }

            if (!string.IsNullOrEmpty(filter.Model) )
            {
                filterQuery += string.Format(" c.Model = '{0}' and", filter.Model);
            }
            if (!string.IsNullOrEmpty(filter.Make))
            {
                filterQuery += string.Format(" c.Make = '{0}' and", filter.Make);
            }
            if (!string.IsNullOrEmpty(filter.RegId))
            {
                filterQuery += string.Format(" c.RegId = '{0}' and", filter.RegId);
            }
            if (!string.IsNullOrEmpty(filter.ChassisNo))
            {
                filterQuery += string.Format(" c.ChassisNo = '{0}' and", filter.ChassisNo);
            }
            if (!string.IsNullOrEmpty(filter.FuelSystem) && filter.FuelSystem != "-1")
            {
                filterQuery += string.Format(" c.FuelSystem = '{0}' and", filter.FuelSystem);
            }
        
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                searchTextQuery = " c.RegId like '%" + filter.SearchText + "%' or c.CC like '%" + filter.SearchText + "%' or c.Model like '%" + filter.SearchText + "%' or c.Make like '%" + filter.SearchText + "%' or  c.Financier like '%" + filter.SearchText + "%' or c.FuelSystem like '%" + filter.SearchText + "%' or c.ChassisNo like '%" + filter.SearchText + "%' or c.CreatedDate like '%" + filter.SearchText + "%' and ";
                CountTextQuery = " where c.RegId like '%" + filter.SearchText + "%' or c.RegId like '%" + filter.SearchText + "%' or c.CC like '%" + filter.SearchText + "%' or c.Model like '%" + filter.SearchText + "%' or c.Make like '%" + filter.SearchText + "%' or  c.Financier like '%" + filter.SearchText + "%' or c.FuelSystem like '%" + filter.SearchText + "%' or c.ChassisNo like '%" + filter.SearchText + "%' or c.CreatedDate like '%" + filter.SearchText + "%' ";
            }

            List<Car> OpportunityList = new List<Car>();
          
            string rawQuery = @"  
                                declare @pagesize int
                                declare @pageno int 
                                set @pagesize = " + filter.UnitPerPage + @"
                                set @pageno = " + filter.PageNumber + @"
                                declare @pagestart int
                                set @pagestart=(@pageno-1)* @pagesize  
                                select  TOP (@pagesize) c.*,
                                CASE WHEN cuser.ConcernName is null  THEN  u.Name else cuser.ConcernName end as UserName,
                
                                co.ConcernName as ConcernName FROM Cars c
                           		left join UserCarMaps um on um.CarId = c.CarId
                                left join Users u on u.UserId = um.UserId
                                left join Concerns cuser on cuser.ConcernId = um.UserId
                                left join Concerns co on co.ConcernId = c.CompanyId
                                where {1}{2}  c.Id NOT IN(Select TOP (@pagestart) Id from Cars {0})
                                {0}
                      ";

            string CountQuery = string.Format("Select * from Cars c {0}", CountTextQuery);

            

            #region Order
            if (!string.IsNullOrWhiteSpace(filter.Order))
            {
                if (filter.Order == "ascending/make")
                {
                    subquery = "order by Make asc";

                }
                else if (filter.Order == "descending/make")
                {
                    subquery = "order by Make desc";

                }
                else if (filter.Order == "ascending/model")
                {
                    subquery = "order by Model asc";

                }
                else if (filter.Order == "descending/model")
                {
                    subquery = "order by Model desc";

                }
                else if (filter.Order == "ascending/regid")
                {
                    subquery = "order by RegId asc";

                }
                else if (filter.Order == "descending/regid")
                {
                    subquery = "order by RegId desc";

                }
                else if (filter.Order == "ascending/ligalowner")
                {
                    subquery = "order by LigalOwner asc";

                }
                else if (filter.Order == "descending/ligalowner")
                {
                    subquery = "order by LigalOwner desc";

                }
                else if (filter.Order == "ascending/fuelsystem")
                {
                    subquery = "order by FuelSystem asc";

                }
                else if (filter.Order == "descending/fuelsystem")
                {
                    subquery = "order by FuelSystem desc";

                }
                else if (filter.Order == "ascending/CreatedDate")
                {
                    subquery = "order by CreatedDate asc";

                }
                else if (filter.Order == "descending/CreatedDate")
                {
                    subquery = "order by CreatedDate desc";

                }
              
            }
            else
            {
                subquery = "order by Id desc";
                subquery1 = "order by Id desc";
            }
            #endregion

            rawQuery = string.Format(rawQuery, subquery, searchTextQuery, filterQuery);
            List<Car> dsResult = context.Database.SqlQuery<CarVM>(rawQuery, new object[] { }).ToList<Car>();

            //List<EF.Person> listPerson = dbEntities.Database.SqlQuery<EF.Person_Reflect>(sql, new object[] { }).ToList<EF.Person>();

            //List<CarVM> dsResult = context.Set<CarVM>().SqlQuery(rawQuery).ToList<CarVM>();
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.CarList = dsResult;
            int TotalCount = context.Set<Car>().SqlQuery(CountQuery).ToList().Count;
            //context.Dispose();
            vehicleModel.TotalCount = TotalCount;
           
            return vehicleModel;
             
            
        }

        public List<Drivers> GetAllAllocationsbyQuery(string query)
        {
         
         

            List<Car> OpportunityList = new List<Car>();

            string rawQuery = @"  
                                
                               
                                select  * FROM Drivers 
                           
                                where  (Name like '%{0}%' OR MobileNumber like '%{0}%' OR Email like '%{0}%')
								
                               ";


            string sqlQuery = string.Format(rawQuery, query);


        
            List<Drivers> dsResult = context.Set<Drivers>().SqlQuery(sqlQuery).ToList();
         
     
            return dsResult;


        }
        public List<Car> GetAllCarsbyQuery(string query)
        {
            List<Car> CarList = new List<Car>();

            string rawQuery = @"  
                                
                               
                                select  * FROM Cars 
                           
                                where  (Make like '%{0}%' OR Model like '%{0}%' OR RegId like '%{0}%')
								
                               ";


            string sqlQuery = string.Format(rawQuery, query);



            List<Car> dsResult = context.Set<Car>().SqlQuery(sqlQuery).ToList();


            return dsResult;


        }

        public List<Car> GetAllCarsbyIdList(List<string> idList)
        {
            string ids = "";
            if(idList.Count > 0)
            {
                foreach(var item in idList)
                {
                    ids += item + ",";
                }
                ids = ids.Remove(ids.Length - 1);
            }

            string rawQuery = @"  
                                select  * FROM Cars  
                           
                                where Id in ({0})
								
                               ";


            string sqlQuery = string.Format(rawQuery, ids);



            List<Car> dsResult = context.Set<Car>().SqlQuery(sqlQuery).ToList();


            return dsResult;


        }
        public List<Users> GetAllOwnersbyQuery(string query)
        {



            List<Users> OpportunityList = new List<Users>();

            string rawQuery = @"  
                                
                               
                                select  * FROM Users u
                           
                                where  (Name like '%{0}%' OR Mobile like '%{0}%' OR Email like '%{0}%')
								
                               ";


            string sqlQuery = string.Format(rawQuery, query);



            List<Users> dsResult = context.Set<Users>().SqlQuery(sqlQuery).ToList();


            return dsResult;


        }
        public Car GetVehicleById(int id)
        {
            
            List<Users> OpportunityList = new List<Users>();

            string rawQuery = @"select   c.*,
                                d.Name as DriverName,
                                CASE WHEN d.DriverId is null  THEN '00000000-0000-0000-0000-000000000000' else d.DriverId end as DriverId,
                                  CASE WHEN cuser.ConcernName is null  THEN  u.Name else cuser.ConcernName end as UserName,
                                CASE WHEN u.UserId  is null  THEN '00000000-0000-0000-0000-000000000000' else u.UserId end as UserId,
                                co.ConcernName as ConcernName
                                 FROM Cars c
                                left join UserDriverMaps ud on ud.CarId = c.CarId 
                                left join Drivers d on d.DriverId = ud.DriverId
                          
                                left join UserCarMaps um on um.CarId = c.CarId
                                left join Users u on u.UserId = um.UserId
                                left join Concerns cuser on cuser.ConcernId = um.UserId
                                left join Concerns co on co.ConcernId = c.CompanyId
                                where c.Id = {0}
                               ";


            string sqlQuery = string.Format(rawQuery, id);
            //List<Users> dsResult = context.Set<Users>().SqlQuery(sqlQuery).ToList();
            Car dsResult = context.Database.SqlQuery<CarVM>(sqlQuery, new object[] { }).ToList<Car>().FirstOrDefault();

            return dsResult;


        }

        public Car GetVehicleByCarId(Guid CarId)
        {

            List<Users> OpportunityList = new List<Users>();

            string rawQuery = @"select   c.*,
                                d.Name as DriverName,
                                CASE WHEN d.DriverId is null  THEN '00000000-0000-0000-0000-000000000000' else d.DriverId end as DriverId,
                                u.Name as UserName,
                                CASE WHEN u.UserId  is null  THEN '00000000-0000-0000-0000-000000000000' else u.UserId end as UserId

                                 FROM Cars c
                                left join UserDriverMaps ud on ud.CarId = c.CarId 
                                left join Drivers d on d.DriverId = ud.DriverId
                                left join UserCarMaps um on um.CarId = c.CarId
                                left join Users u on u.UserId = um.UserId
                                where c.CarId = '{0}'
                               ";


            string sqlQuery = string.Format(rawQuery, CarId);
            //List<Users> dsResult = context.Set<Users>().SqlQuery(sqlQuery).ToList();
            Car dsResult = context.Database.SqlQuery<CarVM>(sqlQuery, new object[] { }).ToList<Car>().FirstOrDefault();

            return dsResult;


        }
    }


}
