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
        PaymentFacade payFacade = null;
        SalesOrderDetailsFacade salesDetailFacade = null;
        public InvoiceController()
        {
            DataContext Context = DataContext.getInstance();
            salesFacade = new SalesOrderFacade(Context);
            salesDetailFacade = new SalesOrderDetailsFacade(Context);
            payFacade = new PaymentFacade(Context);

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
        public ActionResult InvoiceListPartial(Guid CustomerId,string InvoiceType)
        {
            ViewBag.CustomerId = CustomerId;
            List<PaymentReceive> PaymentList = new List<PaymentReceive>();
            PaymentList = payFacade.GetAllPaymentReceiveByCustomerId(CustomerId);
            return View(PaymentList);
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
                    List<SalesOrderDetailVM> salesDetailList = salesDetailFacade.GetAllSalesDetailsBySaleOrderId(salesOrder.SalesOrderId);
                    salesOrderModel.SalesOrder = salesOrder;
                    salesOrderModel.SalesOrderDetailList = salesDetailList;
                }
            }
            else
            {
                salesOrderModel.SalesOrder = salesOrder;
                salesOrderModel.SalesOrderDetailList = new List<SalesOrderDetailVM>();
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
                    SalesOrderModel.SalesOrder.WarehouseId = Guid.NewGuid();
                    SalesOrderModel.SalesOrder.OrderDate = DateTime.Now;
                    SalesOrderModel.SalesOrder.DelivaryDate = DateTime.Now;
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

                    PaymentReceive payment = new PaymentReceive();
                    payment.PaymentId = Guid.NewGuid();
                    payment.SalesOrderId = SalesOrderModel.SalesOrder.SalesOrderId;
                    payment.BalanceDue = SalesOrderModel.SalesOrder.Total;
                    payment.PaymentAmount = SalesOrderModel.SalesOrder.Amount;

                    payment.PaymentStatus = "UnPaid";
                    payment.PaymentDate = DateTime.Now;
                    payFacade.Insert(payment);


                }
                else
                {
                    SalesOrder sales = salesFacade.Get(SalesOrderModel.SalesOrder.Id);
                    sales.OrderDate = SalesOrderModel.SalesOrder.OrderDate;
                    sales.DelivaryDate = SalesOrderModel.SalesOrder.DelivaryDate;
                    sales.DiscountAmount = SalesOrderModel.SalesOrder.DiscountAmount;
                    sales.Amount = SalesOrderModel.SalesOrder.Amount;
                    sales.Total = SalesOrderModel.SalesOrder.Total;
                    salesFacade.Update(sales);
                    List<SalesOrderDetailVM> salesdetaillist = salesDetailFacade.GetAllSalesDetailsBySaleOrderId(sales.SalesOrderId);
                    if (SalesOrderModel.SalesOrderDetailList.Count > 0)
                    {
                        SalesOrderDetail tempSalesOrderDetail = new SalesOrderDetail();
                        foreach (var item in salesdetaillist)
                        {
                            salesDetailFacade.Delete(item.Id);
                        }
                        foreach (var item in SalesOrderModel.SalesOrderDetailList)
                        {
                            tempSalesOrderDetail.Price = item.Price;
                            tempSalesOrderDetail.ProductId = item.ProductId;
                            tempSalesOrderDetail.Quantity = item.Quantity;
                            tempSalesOrderDetail.SalesOrderDetailId = Guid.NewGuid();
                            tempSalesOrderDetail.SalesOrderId = SalesOrderModel.SalesOrder.SalesOrderId;
                            tempSalesOrderDetail.SubTotal = item.SubTotal;
                            tempSalesOrderDetail.Total = item.Total;
                            tempSalesOrderDetail.Amount = item.Amount;

                            salesDetailFacade.Insert(tempSalesOrderDetail);
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