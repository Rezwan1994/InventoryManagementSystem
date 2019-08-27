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

        public InvoiceController()
        {
            DataContext Context = DataContext.getInstance();
            salesFacade = new SalesOrderFacade(Context);

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
        public ActionResult AddInvoice(Guid CustomerId)
        {
            SalesOrder salesOrder = new SalesOrder();
            return View(salesOrder);
        }
    }
}