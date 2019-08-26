using IMSRepository;
using SFMS.Entity;
using SFMS.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.WEB.UI.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        ProductsFacade productsFacade = null;
        WareHouseFacade wareHouseFacade = null;
        PWMFacade pWMFacade = null;
        public ProductsController()
        {
            DataContext Context = DataContext.getInstance();
            productsFacade = new ProductsFacade(Context);
            wareHouseFacade = new WareHouseFacade(Context);
            pWMFacade = new PWMFacade(Context);
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Products/Details/5
        public ActionResult ProductsPartial()
        {
            return View();
        }

        #region Products

        public ActionResult LoadProductPartial()
        {
            return PartialView();
        }

        public ActionResult LoadProductList(ProductsFilter filter)
        {
            if (filter.PageNumber == 0)
            {
                filter.PageNumber = 1;
            }
            filter.UnitPerPage = 12;

            if (filter.PageNumber == null || filter.PageNumber == 0)
            {
                filter.PageNumber = 1;
            }
            ProductsModel productsList = productsFacade.GetProducts(filter);

            ViewBag.OutOfNumber = productsList.TotalCount;
            if ((int)ViewBag.OutOfNumber == 0)
            {
                ViewBag.Message = "No Content Available !";
            }
            if (@ViewBag.OutOfNumber == 0)
            {
                filter.PageNumber = 1;
            }
            ViewBag.PageNumber = filter.PageNumber;

            if ((int)ViewBag.PageNumber * filter.UnitPerPage > (int)ViewBag.OutOfNumber)
            {
                ViewBag.CurrentNumber = (int)ViewBag.OutOfNumber;
            }
            else
            {
                ViewBag.CurrentNumber = (int)ViewBag.PageNumber * filter.UnitPerPage;
            }

            ViewBag.PageCount = Math.Ceiling((double)ViewBag.OutOfNumber / filter.UnitPerPage.Value);
            return View(productsList.ProductsList);
        }

        public ActionResult AddProduct(int? id)
        {
            Product model = new Product();
            if (id.HasValue && id > 0)
            {
                model = productsFacade.Get(id.Value);
            }
            return View(model);
        }

        public JsonResult SaveProduct(Product newProduct)
        {
            var result = false;
            if (newProduct != null)
            {
                if (newProduct.Id > 0)
                {
                    var oldProduct = productsFacade.Get(newProduct.Id);
                    oldProduct.ImageUrl = newProduct.ImageUrl;
                    oldProduct.ProductName = newProduct.ProductName;
                    oldProduct.Quantity = newProduct.Quantity;
                    oldProduct.SellingPrice = newProduct.SellingPrice;
                    oldProduct.BuyingPrice = newProduct.BuyingPrice;
                    oldProduct.Category = newProduct.Category;
                    oldProduct.SubCategory = newProduct.SubCategory;

                    if (productsFacade.Update(oldProduct) > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    newProduct.ProductId = Guid.NewGuid();
                    if (productsFacade.Insert(newProduct) > 0)
                    {
                        result = true;
                    }
                }
            }
            return Json(new { result = result });
        }

        public JsonResult DeleteProduct(int id)
        {
            bool result = productsFacade.Delete(id) > 0;
            string message = "Something Wrong!!";
            if (result)
            {
                message = "Product deleted Sucessfully";
            }
            return Json(new { result = result, message = message });
        }

        public ActionResult GetProductsList()
        {
            string query = Request.QueryString["q[term]"] != null ? Request.QueryString["q[term]"].ToString() : "";
            List<Product> ProductList = productsFacade.GetAllProductsbyQuery(query);

            foreach (var item in ProductList)
            {
                List<ProductWarehouseMap> pwmList = pWMFacade.GetAll().Where(x => x.ProductId == item.ProductId).ToList();
                if (pwmList.Count > 0)
                {
                    item.RemainQantity = item.Quantity - pwmList.Sum(x => Convert.ToInt32(x.Quantity));
                }
                else
                {
                    item.RemainQantity = item.Quantity;
                }
                if (item.RemainQantity <= 0)
                {
                    ProductList.Remove(item);
                }
                if (ProductList.Count() <= 0)
                    break;
            }
            var CarNameList = ToDataTable(ProductList);

            List<Dictionary<string, object>>
             lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in CarNameList.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in CarNameList.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }

            return Json(lstRows, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Warehouse

        public ActionResult LoadWarehousePartial()
        {
            return PartialView();
        }
        public ActionResult LoadWarehouseList(WarehouseFilter filter)
        {
            if (filter.PageNumber == 0)
            {
                filter.PageNumber = 1;
            }
            filter.UnitPerPage = 12;

            if (filter.PageNumber == null || filter.PageNumber == 0)
            {
                filter.PageNumber = 1;
            }
            WarehouseModel warehouseList = wareHouseFacade.GetWareHouses(filter);

            ViewBag.OutOfNumber = warehouseList.TotalCount;
            if ((int)ViewBag.OutOfNumber == 0)
            {
                ViewBag.Message = "No Content Available !";
            }
            if (@ViewBag.OutOfNumber == 0)
            {
                filter.PageNumber = 1;
            }
            ViewBag.PageNumber = filter.PageNumber;

            if ((int)ViewBag.PageNumber * filter.UnitPerPage > (int)ViewBag.OutOfNumber)
            {
                ViewBag.CurrentNumber = (int)ViewBag.OutOfNumber;
            }
            else
            {
                ViewBag.CurrentNumber = (int)ViewBag.PageNumber * filter.UnitPerPage;
            }

            ViewBag.PageCount = Math.Ceiling((double)ViewBag.OutOfNumber / filter.UnitPerPage.Value);
            return View(warehouseList.WareHouseList);
        }

        [HttpGet]
        public ActionResult AddWarehouse(int? id)
        {
            WareHouse model = new WareHouse();
            if (id.HasValue && id > 0)
            {
                model = wareHouseFacade.Get(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult SaveWarehouse(WareHouse wareHouse)
        {

            var result = false;
            if (wareHouse != null)
            {
                if (wareHouse.Id > 0)
                {
                    var oldWareHouse = wareHouseFacade.Get(wareHouse.Id);
                    oldWareHouse.WarehouseName = wareHouse.WarehouseName;
                    oldWareHouse.Address = wareHouse.Address;
                    oldWareHouse.Description = wareHouse.Description;
                    if (wareHouseFacade.Update(oldWareHouse) > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    wareHouse.WarehouseId = Guid.NewGuid();
                    if (wareHouseFacade.Insert(wareHouse) > 0)
                    {
                        result = true;
                    }
                }
            }
            return Json(new { result = result });
        }

        public JsonResult DeleteWarehouse(int id)
        {
            bool result = wareHouseFacade.Delete(id)>0;
            string message = "Something Wrong!!";
            if (result)
            {
                message = "Warehouse deleted Sucessfully";
            }
            return Json(new { result = result, message= message });
        }

        public ActionResult GetWarehousesList()
        {
            string query = Request.QueryString["q[term]"] != null ? Request.QueryString["q[term]"].ToString() : "";
            List<WareHouse> WarehouseList = wareHouseFacade.GetAllWarehousesbyQuery(query);

            var CarNameList = ToDataTable(WarehouseList);

            List<Dictionary<string, object>>
             lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in CarNameList.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in CarNameList.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }

            return Json(lstRows, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Product Warehouse Map
        public ActionResult LoadPWMPartial()
        {
            return PartialView();
        }

        public ActionResult LoadPWMList(PWMsFilter filter)
        {
            if (filter.PageNumber == 0)
            {
                filter.PageNumber = 1;
            }
            filter.UnitPerPage = 12;

            if (filter.PageNumber == null || filter.PageNumber == 0)
            {
                filter.PageNumber = 1;
            }
            PWMsModel pwmsList = productsFacade.GetPWM(filter);

            ViewBag.OutOfNumber = pwmsList.TotalCount;
            if ((int)ViewBag.OutOfNumber == 0)
            {
                ViewBag.Message = "No Content Available !";
            }
            if (@ViewBag.OutOfNumber == 0)
            {
                filter.PageNumber = 1;
            }
            ViewBag.PageNumber = filter.PageNumber;

            if ((int)ViewBag.PageNumber * filter.UnitPerPage > (int)ViewBag.OutOfNumber)
            {
                ViewBag.CurrentNumber = (int)ViewBag.OutOfNumber;
            }
            else
            {
                ViewBag.CurrentNumber = (int)ViewBag.PageNumber * filter.UnitPerPage;
            }

            ViewBag.PageCount = Math.Ceiling((double)ViewBag.OutOfNumber / filter.UnitPerPage.Value);
            return View(pwmsList.PWMList);
        }

        public ActionResult AddPWM(int? id)
        {
            ProductWarehouseMap model = new ProductWarehouseMap();
            List<SelectListItem> ProductList = new List<SelectListItem>();
            List<SelectListItem> WarehouseList = new List<SelectListItem>();
            if (id == null)
            {
                ProductList.Add(new SelectListItem
                {
                    Text = "Products",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.ProductList = ProductList;
                WarehouseList.Add(new SelectListItem
                {
                    Text = "Warehouses",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.WarehouseList = WarehouseList;
            }

            //ViewBag.ProductList = productsFacade.GetAll().Select(x =>
            //          new SelectListItem()
            //          {
            //              Text = x.ProductName.ToString() + "(" + x.Category + " - " + x.SubCategory + ")",
            //              Value = x.ProductId.ToString()
            //          }).ToList();

            //ViewBag.WarehouseList = wareHouseFacade.GetAll().Select(x =>
            //          new SelectListItem()
            //          {
            //              Text = x.WarehouseName.ToString(),
            //              Value = x.WarehouseId.ToString()
            //          }).ToList();

            if (id.HasValue && id > 0)
            {
                model = pWMFacade.Get(id.Value);
                WareHouse wareHouse = wareHouseFacade.GetAll().Where(x => x.WarehouseId == model.WarehouseId).FirstOrDefault();
                WarehouseList.Add(new SelectListItem
                {
                    Text = wareHouse.WarehouseName,
                    Value = wareHouse.WarehouseId.ToString(),
                    Selected = true
                });
                ViewBag.WarehouseList = WarehouseList;

                Product product = productsFacade.GetAll().Where(x => x.ProductId == model.ProductId).FirstOrDefault();
                ProductList.Add(new SelectListItem
                {
                    Text = product.ProductName,
                    Value = product.ProductId.ToString(),
                    Selected = true
                });
                ViewBag.ProductList = ProductList;
            }
            return View(model);
        }

        public JsonResult SavePWM(ProductWarehouseMap newPWM)
        {
            var result = false;
            if (newPWM != null)
            {
                if (newPWM.Id > 0)
                {
                    var oldPWM = pWMFacade.Get(newPWM.Id);
                    oldPWM.WarehouseId = newPWM.WarehouseId;
                    oldPWM.ProductId = newPWM.ProductId;
                    oldPWM.Quantity = newPWM.Quantity;
                    if (pWMFacade.Update(oldPWM) > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    var samePWM = pWMFacade.GetAll().Where(x => x.ProductId == newPWM.ProductId && x.WarehouseId == newPWM.WarehouseId).FirstOrDefault();
                    if (samePWM != null)
                    {
                        samePWM.WarehouseId = newPWM.WarehouseId;
                        samePWM.ProductId = newPWM.ProductId;
                        samePWM.Quantity += newPWM.Quantity;
                        if (pWMFacade.Update(samePWM) > 0)
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        newPWM.ProductWarehouseMapId = Guid.NewGuid();
                        if (pWMFacade.Insert(newPWM) > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            return Json(new { result = result });
        }

        public JsonResult DeletePWM(int id)
        {
            bool result = pWMFacade.Delete(id) > 0;
            string message = "Something Wrong!!";
            if (result)
            {
                message = "Selected item deleted Sucessfully";
            }
            return Json(new { result = result, message = message });
        }

        #endregion

        #region Others
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(
            prop.PropertyType) ?? prop.PropertyType);

            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        #endregion


    }
}
