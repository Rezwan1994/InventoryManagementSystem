using SFMS.Entity;
using SFMS.Facade;
using IMS.Repository;
using SmartFleetManagementSystem.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartFleetManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        UserFacade userFacade = null;
        FuelBillFacade fuelBillFacade = null;
        UserLoginFacade userlogin = null;
        public HomeController()
        {
            DataContext Context = DataContext.getInstance();
            userFacade = new UserFacade(Context);
            fuelBillFacade = new FuelBillFacade(Context);
            userlogin = new UserLoginFacade(Context);
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
        public ActionResult Dashboard()
        {
            //This GetAllData method will fetch data from server and create a comma seperate string.
            var FuelList = fuelBillFacade.GetAll();
            var data = "date,value\r\n";
            foreach (var item in FuelList)
            {
                var tempDate = item.IssueDate.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                data += tempDate.Split('T')[0] + "," + item.TotalCost;
                data += "\r\n";
            }
            data += "\r\n";
            string tempFolderPath = Server.MapPath("~/Files/");
            if (FileHelper.CreateFolderIfNeeded(tempFolderPath) == "1")
            {
                try
                {
                    tempFolderPath += "data.csv";
                    fuelBillFacade.WriteCSVFile(data, tempFolderPath);
                }
                catch (Exception ex) {  /*TODO: You must process this exception.*/}
            }
            return PartialView();
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