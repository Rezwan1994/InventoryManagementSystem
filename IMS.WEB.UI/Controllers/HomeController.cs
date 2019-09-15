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
            dashboardViewModel.products_count = productsFacade.GetAll().Count();
            dashboardViewModel.warehouses_count = wareHouseFacade.GetAll().Count();
            dashboardViewModel.users_count = userFacade.GetAll().Count();
            List<Product> FinishedProduct = productsFacade.GetAll().Where(x => x.Quantity <= 5).ToList();
            List<PaymentReceive> UnpaidInvoices = paymentFacade.GetAll().Where(x => x.PaymentStatus == "Unpaid" || x.PaymentStatus== "Partially Paid").Take(5).ToList();
            dashboardViewModel.FinishedProducts = FinishedProduct;
            dashboardViewModel.UnpaidInvoices = UnpaidInvoices;
            return View(dashboardViewModel);
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