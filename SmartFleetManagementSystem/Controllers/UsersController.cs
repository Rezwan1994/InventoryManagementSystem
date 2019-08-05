using SFMS.Entity;
using SFMS.Facade;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmartFleetManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        UserFacade usersFacade = null;
        UserCarMapFacade carMapFacade = null;
        public UsersController()
        {
            DataContext Context = DataContext.getInstance();
            usersFacade = new UserFacade(Context);
            carMapFacade = new UserCarMapFacade(Context);
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

        public ActionResult UsersList()
        {
            return View();
        }
        public ActionResult LoadUsersList(UsersFilter filter)
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
            UsersModel UsersList = usersFacade.GetUsers(filter);

            ViewBag.OutOfNumber = UsersList.TotalCount;
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
            return View(UsersList.UsersList);
        }

        public ActionResult AddUser(int? id)
        {
            Users model = new Users();
            if (id.HasValue && id > 0)
            {
                model = usersFacade.Get(id.Value);
            }
            return View(model);
        }


        public JsonResult SaveUser(Users Users)
        {

            var result = false;
            if (Users != null)
            {
                if (Users.Id > 0)
                {
                    var oldUsers = usersFacade.Get(Users.Id);
                    Users.UserId = oldUsers.UserId;
                    Users.CreatedDate = oldUsers.CreatedDate;
                    Users.ImgSrc = oldUsers.ImgSrc;
                    Users.IsActive = oldUsers.IsActive;
                    if (usersFacade.Update(Users) > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    Users.CreatedDate = DateTime.Now;
                    Users.UserId = Guid.NewGuid();
                    Users.ImgSrc = "";
                    Users.IsActive = true;
                    if (usersFacade.Insert(Users) > 0)
                    {
                        result = true;
                    }
                }
            }
            return Json(new { result = result });
        }

        public JsonResult DeleteUser(int Id)
        {
            bool result = false;
            string message = "";
            Users user = usersFacade.Get(Id);
            List<UserCarMap> MapList = carMapFacade.GetUserCarMapByUserId(user.UserId);
            try
            {
                usersFacade.Delete(Id);
                foreach(var item in MapList)
                {
                    carMapFacade.Delete(item.Id);
                }
                result = true;
                message = "User deleted successfully";

            }
            catch (Exception ex)
            {
                result = false;
                message = "Internal Error!";
            }
            return Json(new { result = result, message = message });

        }
    }
}