using SFMS.Entity;
using SFMS.Facade;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartFleetManagementSystem.Controllers
{
    public class ConcernController : Controller
    {
        // GET: Concern

        ConcernsFacade ConcernFacade = null;
        LookUpFacade lookupFacade = null;
        public ConcernController()
        {
            DataContext Context = DataContext.getInstance();
            ConcernFacade = new ConcernsFacade(Context);
            lookupFacade = new LookUpFacade(Context);


        }
        // GET: Fuel
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
        public ActionResult ConcernsList()
        {

            return View();
        }
        public ActionResult LoadConcernList(ConcernFilter filter)
        {

            if (filter.PageNumber == 0)
            {
                filter.PageNumber = 1;
            }
            filter.UnitPerPage = 10;

            if (filter.PageNumber == null || filter.PageNumber == 0)
            {
                filter.PageNumber = 1;
            }
            ConcernModel ConcernList = ConcernFacade.GetConcerns(filter);
            List<string> idList = new List<string>();
            foreach (var item in ConcernList.ConcernsList)
            {
                idList.Add(item.Id.ToString());
            }
            //Need to change
            System.Web.HttpRuntime.Cache["GetAllVehicleIdList"] = idList;
            ViewBag.OutOfNumber = ConcernList.TotalCount;
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



            return View(ConcernList.ConcernsList);
        }

        public ActionResult AddConcern(int? id)
        {
            Concerns model = new Concerns();
            if (id.HasValue && id > 0)
            {
                model = ConcernFacade.Get(id.Value);
               
            }
          else
            {
                model = new Concerns();
            }

            return View(model);
        }

        public JsonResult SaveConcern(Concerns Concern)
        {
            bool result = false;
            string message = "";
            try
            {
                #region Car Driver Map
                Concerns ump = new Concerns();
                if (Concern.Id > 0)
                {
                    ump = ConcernFacade.Get(Concern.Id);
                    ump.Phone = Concern.Phone;
                    ump.Address = Concern.Address;
                    ump.ConcernType = Concern.ConcernType;
                    ump.ConcernName = Concern.ConcernName;
              
                    ConcernFacade.Update(ump);
                    result = true;
                    message = "Concern Updated successfully.";
                }
                else
                {
                    Concern.ConcernId = Guid.NewGuid();

                    ConcernFacade.Insert(Concern);
                    result = true;
                    message = "Concern added successfully.";
                }

                #endregion
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