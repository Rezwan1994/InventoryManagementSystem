using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSRepository
{
    public class ProductRepository : Repository<Product>
    {
        DataContext context = null;
        public ProductRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public ProductsModel GetProducts(ProductsFilter filter)
        {
            string searchTextQuery = "";
            string subquery = "";
            string filterQuery = "";
            string CountTextQuery = "";

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                searchTextQuery = " c.ProductName like '%" + filter.SearchText + "%' or c.Category like '%" + filter.SearchText + "%' or c.SubCategory like '%" + filter.SearchText + "%' or c.Quantity like '%" + filter.SearchText + "%' and ";
                CountTextQuery = " where c.ProductName like '%" + filter.SearchText + "%' or c.Category like '%" + filter.SearchText + "%' or c.SubCategory like '%" + filter.SearchText + "%' or c.Quantity like '%" + filter.SearchText + "%' and ";
            }

            string rawQuery = @"  
                                declare @pagesize int
                                declare @pageno int 
                                set @pagesize = " + filter.UnitPerPage + @"
                                set @pageno = " + filter.PageNumber + @"
                                declare @pagestart int
                                set @pagestart=(@pageno-1)* @pagesize  
                                select  TOP (@pagesize) c.* FROM Products c
                           
                                where {1}{2}  c.Id NOT IN(Select TOP (@pagestart) Id from Products {0})
                                {0}
                               ";

            string CountQuery = string.Format("Select * from Products c {0}", CountTextQuery);

            rawQuery = string.Format(rawQuery, subquery, searchTextQuery, filterQuery);
            int TotalCount = 0;
            List<Product> dsResult = new List<Product>();
            try
            {
                var ctx = DataContext.getInstance();
                dsResult = ctx.Product.SqlQuery(rawQuery).ToList();
                TotalCount = ctx.Product.SqlQuery(CountQuery).ToList().Count;
            }
            catch (Exception ex)
            {

            }

            ProductsModel productsModel = new ProductsModel();
            productsModel.ProductsList = dsResult;

            //context.Dispose();
            productsModel.TotalCount = TotalCount;
            return productsModel;
        }


        public List<Product> GetProductsByKey(string Key,string ExistEquipment,Guid Warehouse)
        {
            string searchTextQuery = "";
        
            string CountTextQuery = "";

            if (!string.IsNullOrWhiteSpace(Key))
            {
                searchTextQuery = " c.ProductName like '%" + Key + "%' or c.Category like '%" + Key + "%' or c.SubCategory like '%" + Key + "%' or c.Quantity like '%" + Key + "%' ";
              
            }

            string rawQuery = @" 
                                select c.*,pm.Quantity as QuantityOnHand , wh.WarehouseName as WarehouseName,wh.WarehouseId as WarehouseId
                                from Products c 
                                left join ProductWarehouseMaps pm on c.ProductId = pm.ProductId
                                left join WareHouses wh on wh.WarehouseId = pm.WarehouseId
                                where  pm.WarehouseId = '{2}' and ({0}){1}
                               ";
            var EqpExist = "";
            if (!string.IsNullOrEmpty(ExistEquipment))
            {
                EqpExist = string.Format(" AND (c.ProductId not in {0})", ExistEquipment);
            }

            string CountQuery = string.Format("Select * from Products c {0}", CountTextQuery);

            rawQuery = string.Format(rawQuery,searchTextQuery, EqpExist, Warehouse);
            int TotalCount = 0;
            List<Product> dsResult = new List<Product>();
            try
            {
                var ctx = DataContext.getInstance();
                dsResult = ctx.Database.SqlQuery<ProductVM>(rawQuery, new object[] { }).ToList<Product>();
                TotalCount = ctx.Product.SqlQuery(CountQuery).ToList().Count;
            }
            catch (Exception ex)
            {

            }

            List<Product> ProductsList = new List<Product>();
            ProductsList = dsResult;

        
            return ProductsList;
        }
        public PWMsModel GetPWM(PWMsFilter filter)
        {
            string searchTextQuery = "";
            string subquery = "";
            string filterQuery = "";
            string CountTextQuery = "";

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                searchTextQuery = " P.ProductName like '%" + filter.SearchText + "%' or P.Category like '%" + filter.SearchText + "%' or P.SubCategory like '%" + filter.SearchText + "%' or c.Quantity like '%" + filter.SearchText + "%' or W.WarehouseName like '%" + filter.SearchText + "%' and ";
                CountTextQuery = " where P.ProductName like '%" + filter.SearchText + "%' or P.Category like '%" + filter.SearchText + "%' or P.SubCategory like '%" + filter.SearchText + "%' or c.Quantity like '%" + filter.SearchText + "%' or W.WarehouseName like '%" + filter.SearchText + "%' and ";
            }

            string rawQuery = @"  
                                declare @pagesize int
                                declare @pageno int 
                                set @pagesize = " + filter.UnitPerPage + @"
                                set @pageno = " + filter.PageNumber + @"
                                declare @pagestart int
                                set @pagestart=(@pageno-1)* @pagesize  
                                select  TOP (@pagesize) c.*, P.*, W.* FROM ProductWarehouseMaps c
                                
                                left join Products P on P.ProductId=C.productId
                                left join Warehouses W on W.WarehouseId=C.WarehouseId

                           
                                where {1}{2}  c.Id NOT IN(Select TOP (@pagestart) Id from ProductWarehouseMaps {0})
                                {0}
                               ";

            string CountQuery = string.Format("Select * from ProductWarehouseMaps c {0}", CountTextQuery);

            rawQuery = string.Format(rawQuery, subquery, searchTextQuery, filterQuery);
            int TotalCount = 0;
            List<PWMvm> dsResult = new List<PWMvm>();
            try
            {
                var ctx = DataContext.getInstance();
                dsResult = context.Database.SqlQuery<PWMvm>(rawQuery, new object[] { }).ToList<PWMvm>();
                TotalCount = ctx.ProductWarehouseMap.SqlQuery(CountQuery).ToList().Count;
            }
            catch (Exception ex)
            {

            }

            PWMsModel pwmsModel = new PWMsModel();
            pwmsModel.PWMList = dsResult;

            //context.Dispose();
            pwmsModel.TotalCount = TotalCount;
            return pwmsModel;
        }

        

        public List<Product> GetAllProductsbyQuery(string query)
        {
            List<Product> ConcernList = new List<Product>();

            string rawQuery = @"  
                                select  * FROM Products 
                                where  (ProductName like '%{0}%')
                               ";

            string sqlQuery = string.Format(rawQuery, query);
            List<Product> dsResult = context.Set<Product>().SqlQuery(sqlQuery).ToList();
            return dsResult;
        }
    }
}
