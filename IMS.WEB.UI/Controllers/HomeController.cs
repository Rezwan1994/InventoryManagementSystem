using SFMS.Entity;
using SFMS.Facade;

using SmartFleetManagementSystem.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMSRepository;
using IMS.WEB.UI.Models;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace IMS.WEB.UI.Controllers
{
    public class HomeController : Controller
    {
        UserFacade userFacade = null;
        //FuelBillFacade fuelBillFacade = null;
        UserLoginFacade userlogin = null;
        ProductsFacade productsFacade = null;
        WareHouseFacade wareHouseFacade = null;
        SessionContext sessionContext = null;
        PaymentFacade paymentFacade = null;
        public HomeController()
        {
            DataContext Context = DataContext.getInstance();
            userFacade = new UserFacade(Context);
            userlogin = new UserLoginFacade(Context);
            wareHouseFacade = new WareHouseFacade(Context);
            productsFacade = new ProductsFacade(Context);
            sessionContext = new SessionContext();
            paymentFacade = new PaymentFacade(Context);
        }

        [Authorize]
        public ActionResult Index()
        {
            //if (Session["login_user"] == null)
            //{
            //    return PartialView("~/Views/Shared/_AccessDenied.cshtml");
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }

        public ActionResult DashboardPartial()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.products_count = productsFacade.GetAll().Sum(x=>x.Quantity);
            dashboardViewModel.warehouses_count = wareHouseFacade.GetAll().Count();
            dashboardViewModel.users_count = userFacade.GetAll().Count();
            List<Product> FinishedProduct = productsFacade.GetAll().Where(x => x.Quantity <= 5).ToList();
            List<PaymentReceive> paymentReceives = paymentFacade.GetAll();
            dashboardViewModel.UnpaidInvoices = paymentFacade.GetAll().Where(x => x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid").Take(5).ToList();
            dashboardViewModel.UnpaidInvoiceAmount = dashboardViewModel.UnpaidInvoices.Sum(x => x.BalanceDue);
            dashboardViewModel.PaidInvoiceAmount = paymentReceives.Where(x => x.PaymentStatus == "Paid").ToList().Sum(x => x.PaymentAmount);
            dashboardViewModel.FinishedProducts = FinishedProduct;
            return View(dashboardViewModel);
        }

        [HttpPost]
        public string GetBarChartData()
        {
            List<PaymentReceive> paymentReceives = paymentFacade.GetAll();
            List<GraphData> dataList = new List<GraphData>
            {
                new GraphData {
                    y ="JAN",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 1).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 1).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="FEB",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 2).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 2).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="MAR",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 3).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 3).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="APR",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 4).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 4).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="MAY",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 5).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 5).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="JUN",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 6).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 6).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="JUL",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 7).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 7).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="AUG",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 8).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 8).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="SEP",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 9).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 9).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="OCT",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 10).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 10).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="NOV",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 11).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 11).ToList().Sum(x => x.PaymentAmount)
        },
               new GraphData {
                    y ="DEC",
                    a = paymentReceives.Where(x => (x.PaymentStatus == "UnPaid" || x.PaymentStatus== "Partially Paid") && x.PaymentDate.Month == 12).Sum(x => x.BalanceDue),
                    b = paymentReceives.Where(x => x.PaymentStatus == "Paid" && x.PaymentDate.Month == 12).ToList().Sum(x => x.PaymentAmount)
        },
            };

            var jsonSerializer = new JavaScriptSerializer();
            string data = jsonSerializer.Serialize(dataList);
            return data;
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginAjax(UserLogin user)
        {
            UserLogin authenticatedUser = userlogin.GetAll().Where(x=>x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
            bool result = false;
            if(authenticatedUser != null)
            {
                result = true;
                //Session["login_user"] = user.UserName;
                sessionContext.SetAuthenticationToken(authenticatedUser.UserId.ToString(), false, authenticatedUser);
            }
            else
            {
                result = false;
            }
            return Json(new { result = result});
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult AccessDenied()
        {
            return View("_Layout.cshtml");
          
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}