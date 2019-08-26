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
