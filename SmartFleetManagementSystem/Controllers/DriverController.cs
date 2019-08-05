using SFMS.Entity;
using SFMS.Facade;
using SFMS.Repository;
using System;
using System.Web.Mvc;

namespace SmartFleetManagementSystem.Controllers
{
    public class DriverController : Controller
    {
        DriverFacade driversFacade = null;
        DocumentsFacade documentsFacade = null;
        public DriverController()
        {
            DataContext Context = DataContext.getInstance();
            driversFacade = new DriverFacade(Context);
            documentsFacade = new DocumentsFacade(Context);
        }
        // GET: Driver
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
        public ActionResult DriversList()
        {
            return View();
        }
        public ActionResult LoadDriversList(DriversFilter filter)
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
            DriversModel DriversList = driversFacade.GetDrivers(filter);

            ViewBag.OutOfNumber = DriversList.TotalCount;
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

            #region Documents 
            foreach (var item in DriversList.DriversList)
            {
                item.Documents = documentsFacade.GetByUserId(item.DriverId);
                if(item.Documents== null)
                {
                    item.Documents = new Documents();
                }
            }
            #endregion

            return View(DriversList.DriversList);
        }

        public ActionResult AddDriver(int? id)
        {
            DriverInfo model = new DriverInfo();
            if (id.HasValue && id > 0)
            {
                model.Drivers = driversFacade.Get(id.Value);
                if (model.Drivers != null)
                {
                    model.Documents = documentsFacade.GetByUserId(model.Drivers.DriverId);
                }
                else
                {
                    model.Drivers = new Drivers();
                }
            }
            if (model.Documents == null)
            {
                model.Documents = new Documents();
            }
            if (model.Drivers == null)
            {
                model.Drivers = new Drivers();
            }

            return View(model);
        }
        public JsonResult SaveDriver(DriverInfo driverInfo)
        {

            var result = false;
            #region Driver
            if (driverInfo.Drivers != null)
            {
                if (driverInfo.Drivers.Id > 0)
                {
                    var oldDrivers = driversFacade.Get(driverInfo.Drivers.Id);
                    driverInfo.Drivers.DriverId = oldDrivers.DriverId;
                    driverInfo.Drivers.CreatedDate = oldDrivers.CreatedDate;
                    driverInfo.Drivers.ImgSrc = oldDrivers.ImgSrc;
                    driverInfo.Drivers.IsActive = oldDrivers.IsActive;
                    if (driversFacade.Update(driverInfo.Drivers) > 0)
                    {
                        result = true;
                    }
                }
                else
                {
                    driverInfo.Drivers.CreatedDate = DateTime.Now;
                    driverInfo.Drivers.DriverId = Guid.NewGuid();
                    driverInfo.Drivers.ImgSrc = "";
                    driverInfo.Drivers.IsActive = true;
                    if (driversFacade.Insert(driverInfo.Drivers) > 0)
                    {
                        result = true;
                    }
                }
            }
            #endregion

            #region Documents
            if (driverInfo.Documents != null)
            {
                if (driverInfo.Documents.Id > 0)
                {
                    if (!string.IsNullOrEmpty(driverInfo.Documents.FileSource))
                    {
                        var oldDocuments = documentsFacade.Get(driverInfo.Documents.Id);
                        driverInfo.Documents.DocumentId = oldDocuments.DocumentId;
                        driverInfo.Documents.DocumentsType = oldDocuments.DocumentsType;
                        driverInfo.Documents.UploadedDate = oldDocuments.UploadedDate;
                        driverInfo.Documents.UserId = oldDocuments.UserId;
                        if (documentsFacade.Update(driverInfo.Documents) > 0)
                        {
                            result = true;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(driverInfo.Documents.FileSource))
                    {
                        driverInfo.Documents.DocumentId = Guid.NewGuid();
                        driverInfo.Documents.DocumentsType = DocumentType.DrivingLicense;
                        driverInfo.Documents.UploadedDate = DateTime.Now;
                        driverInfo.Documents.UserId = driverInfo.Drivers.DriverId;
                        if (documentsFacade.Insert(driverInfo.Documents) > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            #endregion
            return Json(new { result = result });
        }
    }
}