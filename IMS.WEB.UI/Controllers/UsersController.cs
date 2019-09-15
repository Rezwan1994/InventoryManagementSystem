using SFMS.Entity;
using SFMS.Facade;
using IMSRepository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace IMS.WEB.UI
{
    public class UsersController : Controller
    {
        // GET: Users
        UserFacade usersFacade = null;
        LookUpFacade lookupFacade = null;
        public UsersController()
        {
            DataContext Context = DataContext.getInstance();
            usersFacade = new UserFacade(Context);
            lookupFacade = new LookUpFacade(Context);
        }
        public ActionResult Index()
        {
            
                return View();
            
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

            ViewBag.CustomerType = lookupFacade.GetLookupByKey("CustomerType").Select(x =>
              new SelectListItem()
              {
                  Text = x.DisplayText.ToString(),
                  Value = x.DataValue.ToString()
              }).ToList();

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
                    oldUsers.Name = Users.Name;
                    oldUsers.Email = Users.Email;
                    oldUsers.Address = Users.Address;
                    oldUsers.Mobile = Users.Mobile;
                    oldUsers.UserType = Users.UserType;
                  
                    if (usersFacade.Update(oldUsers) > 0)
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
            return Json(new { result = result ,UserId = Users.Id});
        }

        public ActionResult LoadUserDetails(int id)
        {
            Users user = new Users();
            if(id > 0 )
            {
                user = usersFacade.Get(id);
            }
        
            return View(user);
        }
    }
}