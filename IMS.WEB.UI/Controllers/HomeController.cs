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

namespace IMS.WEB.UI.Controllers
{
    public class HomeController : Controller
    {
        UserFacade userFacade = null;
        //FuelBillFacade fuelBillFacade = null;
        UserLoginFacade userlogin = null;
        ProductsFacade productsFacade = null;
        WareHouseFacade wareHouseFacade = null;
        public HomeController()
        {
            DataContext Context = DataContext.getInstance();
            userFacade = new UserFacade(Context);
            userlogin = new UserLoginFacade(Context);
            wareHouseFacade = new WareHouseFacade(Context);
            productsFacade = new ProductsFacade(Context);
        }
        public ActionResult Index()
        {
            if (Session["login_user"] == null)
            {
                return PartialView("~/Views/Shared/_AccessDenied.cshtml");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DashboardPartial()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.products_count = productsFacade.GetAll().Count();
            dashboardViewModel.warehouses_count = wareHouseFacade.GetAll().Count();
            dashboardViewModel.users_count = userFacade.GetAll().Count();
            return View(dashboardViewModel);
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginAjax(UserLogin user)
        {
            List<UserLogin> userlist = new List<UserLogin>();
            userlist = userlogin.GetAll().Where(x=>x.UserName == user.UserName && x.Password == user.Password).ToList();
            bool result = false;
            if(userlist.Count()>0)
            {
                result = true;
                Session["login_user"] = user.UserName;
            }
            else
            {
                result = false;
            }
            return Json(new { result = result});
        }
        public ActionResult Logout()
        {
            Session.Clear();
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