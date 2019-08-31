using SFMS.Entity;
using SFMS.Facade;
using IMSRepository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IMS.WEB.UI.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice

        SalesOrderFacade salesFacade = null;
        SalesOrderDetailsFacade salesDetailFacade = null;
        public InvoiceController()
        {
            DataContext Context = DataContext.getInstance();
            salesFacade = new SalesOrderFacade(Context);
            salesDetailFacade = new SalesOrderDetailsFacade(Context);

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InvoicePartial(Guid CustomerId)
        {
            ViewBag.CustomerId = CustomerId;
            return View();
        }
        public ActionResult AddInvoice(Guid CustomerId,Guid? SalesOrderId)
        {
            
            SalesOrder salesOrder = new SalesOrder();
            SalesOrderModel salesOrderModel = new SalesOrderModel();

            if(SalesOrderId != null && SalesOrderId != new Guid())
            {
                salesOrder = salesFacade.GetSalesOrderBySalesOrderId(SalesOrderId.Value);
                if (salesOrder != null)
                {
                    List<SalesOrderDetail> salesDetailList = salesDetailFacade.GetAllSalesDetailsBySaleOrderId(salesOrder.SalesOrderId);
                    salesOrderModel.SalesOrder = salesOrder;
                    salesOrderModel.SalesOrderDetailList = salesDetailList;
                }
            }
            else
            {
                salesOrderModel.SalesOrder = salesOrder;
                salesOrderModel.SalesOrderDetailList = new List<SalesOrderDetail>();
            }
        
    
            ViewBag.CustomerId = CustomerId;
            return View(salesOrderModel);
        }

        [HttpPost]
        public ActionResult AddInvoice(SalesOrderModel SalesOrderModel)
        {
            SalesOrder salesOrder = new SalesOrder();
            bool result = false;
            string massege = "";
            try
            {
                if (SalesOrderModel.SalesOrder.Id == 0)
                {
                    SalesOrderModel.SalesOrder.SalesOrderId = Guid.NewGuid();
                    SalesOrderModel.SalesOrder.CreatedDate = DateTime.Now;
                    SalesOrderModel.SalesOrder.WarehouseId = new Guid();

                    salesFacade.Insert(SalesOrderModel.SalesOrder);
                    if (SalesOrderModel.SalesOrderDetailList.Count > 0)
                    {
                        foreach (var item in SalesOrderModel.SalesOrderDetailList)
                        {
                            item.SalesOrderDetailId = Guid.NewGuid();
                            item.SalesOrderId = SalesOrderModel.SalesOrder.SalesOrderId;

                            salesDetailFacade.Insert(item);
                        }
                    }

                }
                else
                {
                    SalesOrder sales = salesFacade.Get(SalesOrderModel.SalesOrder.Id);
                    sales.OrderDate = SalesOrderModel.SalesOrder.OrderDate;
                    sales.DelivaryDate = SalesOrderModel.SalesOrder.DelivaryDate;
                    sales.DiscountAmount = SalesOrderModel.SalesOrder.DiscountAmount;
                    sales.Amount = SalesOrderModel.SalesOrder.Amount;
                    sales.Total = SalesOrderModel.SalesOrder.Total;
                    List<SalesOrderDetail> salesdetaillist = salesDetailFacade.GetAllSalesDetailsBySaleOrderId(sales.SalesOrderId);
                    if (SalesOrderModel.SalesOrderDetailList.Count > 0)
                    {
                        foreach (var item in salesdetaillist)
                        {
                            salesDetailFacade.Delete(item.Id);
                        }
                        foreach (var item in SalesOrderModel.SalesOrderDetailList)
                        {
                            item.SalesOrderDetailId = Guid.NewGuid();
                            item.SalesOrderId = SalesOrderModel.SalesOrder.SalesOrderId;

                            salesDetailFacade.Insert(item);
                        }
                    }

                }
                result = true;
                massege = "Invoice saved successfully";
            }
            catch(Exception ex)
            {
                result = false;
                massege = "Invoice not saved";
            }
            return Json(new { result = result,message = massege });
        }
    }
}