using SFMS.Entity;
using SFMS.Facade;

using SmartFleetManagementSystem.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMSRepository;

namespace SmartFleetManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        UserFacade userFacade = null;
        //FuelBillFacade fuelBillFacade = null;
        UserLoginFacade userlogin = null;
        public HomeController()
        {
            DataContext Context = DataContext.getInstance();
            userFacade = new UserFacade(Context);
            userlogin = new UserLoginFacade(Context);
        }
        public ActionResult Index()
        {
            //if (Session["login_user"] == null)
            //{
            //    return PartialView("~/Views/Shared/_AccessDenied.cshtml");
            //}
            //else
            //{
                List<UserLogin> userlist = userlogin.GetAll();
                return View();
            //}
          
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginAjax(UserLogin user)
        {
            List<UserLogin> userlist = userlogin.GetAll();
            bool result = false;
            if(!string.IsNullOrEmpty(user.UserName) || !string.IsNullOrEmpty(user.Password))
            {
                foreach(var item in userlist)
                {
                    if((item.UserName == user.UserName) && (item.Password == user.Password))
                    {
                        result = true;
                        Session["login_user"] = user.UserName;
                    }
                    else
                    {
                        result = false;
                    }
                }
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