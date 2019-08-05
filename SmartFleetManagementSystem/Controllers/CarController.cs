using SFMS.Entity;
using SFMS.Facade;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace SmartFleetManagementSystem.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        CarFacade carFacade = null;
        ConcernsFacade concernFacade = null;
        UserCarMapFacade carMapFacade = null;
        UserDriverMapFacade DriverMapFacade = null;
        LookUpFacade lookupFacade = null;
        DocumentsFacade documentsFacade = null;

        public CarController()
        {
            DataContext Context = DataContext.getInstance();
            carFacade = new CarFacade(Context);
            carMapFacade = new UserCarMapFacade(Context);
            DriverMapFacade = new UserDriverMapFacade(Context);
            lookupFacade = new LookUpFacade(Context);
            concernFacade = new ConcernsFacade(Context);
            documentsFacade = new DocumentsFacade(Context);
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
        public ActionResult CarList()
        {
            return View();
        }

        public ActionResult LoadCarList(VehicleFilter filter)
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
            VehicleModel VechleList = carFacade.GetVehicles(filter);
            List<string> idList = new List<string>();
            foreach (var item in VechleList.CarList)
            {
                idList.Add(item.Id.ToString());
            }
            //Need to change
            System.Web.HttpRuntime.Cache["GetAllVehicleIdList"] = idList;
            ViewBag.OutOfNumber = VechleList.TotalCount;
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



            return View(VechleList.CarList);
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(
            prop.PropertyType) ?? prop.PropertyType);

            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        public ActionResult GetAllocaionList()
        {
            string query = Request.QueryString["q[term]"] != null ? Request.QueryString["q[term]"].ToString() : "";
            List<Drivers> DriverList = carFacade.GetAllAllocationsbyQuery(query);

            var AllocationList = ToDataTable(DriverList);

            List<Dictionary<string, object>>
             lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in AllocationList.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in AllocationList.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }

            return Json(lstRows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCarList()
        {
            string query = Request.QueryString["q[term]"] != null ? Request.QueryString["q[term]"].ToString() : "";
            List<Car> CarList = carFacade.GetAllCarsbyQuery(query);

            var CarNameList = ToDataTable(CarList);

            List<Dictionary<string, object>>
             lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in CarNameList.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in CarNameList.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }

            return Json(lstRows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetConcernList()
        {
            string query = Request.QueryString["q[term]"] != null ? Request.QueryString["q[term]"].ToString() : "";
            List<Concerns> ConcernList = concernFacade.GetAllConcernsbyQuery(query);

            var CarNameList = ToDataTable(ConcernList);

            List<Dictionary<string, object>>
             lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in CarNameList.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in CarNameList.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }

            return Json(lstRows, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOwnerList()
        {
            string query = Request.QueryString["q[term]"] != null ? Request.QueryString["q[term]"].ToString() : "";

            List<Users> UserList = carFacade.GetAllOwnersbyQuery(query);
            List<Concerns> ConcernList = concernFacade.GetAllConcernsbyQuery(query);
            var LegalUserList = ToDataTable(UserList);
            var VehicleConcernList = ToDataTable(ConcernList);

            List<Dictionary<string, object>>
             lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in LegalUserList.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in LegalUserList.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }
            foreach (DataRow dr in VehicleConcernList.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in VehicleConcernList.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }
            return Json(lstRows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddDocuments(int? id, string CarId)
        {
            List<SelectListItem> DocumentsType = new List<SelectListItem>();
            Documents documents = new Documents();
            DocumentsType = lookupFacade.GetLookupByKey("DocumentsType").Select(x =>
            new SelectListItem()
            {
                Text = x.DisplayText.ToString(),
                Value = x.DataValue.ToString()
            }).ToList();
            ViewBag.DocumentsType = DocumentsType;
            if (id > 0)
            {
                documents = documentsFacade.Get(id.Value);
                CarId = documents.UserId.ToString();
            }
            ViewBag.CarId = CarId;
            return View(documents);
        }

        [HttpPost]
        public ActionResult SaveDocuments(Documents documents)
        {
            bool result = false;
            string message = "";
            try
            {
                #region Documents
                if (documents.Id > 0)
                {
                    Documents ump = documentsFacade.Get(documents.Id);
                    ump.DocumentsType = documents.DocumentsType;
                    ump.ExpireDate = documents.ExpireDate;
                    ump.IssueDate = documents.IssueDate;
                    ump.LicenseNo = documents.LicenseNo;
                    ump.FileSource = documents.FileSource;
                    ump.UploadedDate = ump.UploadedDate;
                    documentsFacade.Update(ump);
                    result = true;
                    message = "Documents Updated successfully.";
                }
                else
                {
                    if (documents.UserId != null && documents.UserId != new Guid())
                    {
                        documents.DocumentId = Guid.NewGuid();
                        documents.UploadedDate = DateTime.Now;
                        documentsFacade.Insert(documents);
                        result = true;
                        message = "Documents Added successfully.";
                    }
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
        public ActionResult AddVehicle(int? id)
        {
            List<SelectListItem> AllocationList = new List<SelectListItem>();
            List<SelectListItem> LegalOwnerList = new List<SelectListItem>();
            List<SelectListItem> ConcernList = new List<SelectListItem>();
            if (id == null)
            {
                AllocationList.Add(new SelectListItem
                {
                    Text = "Driver",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.AllocationList = AllocationList;
                ConcernList.Add(new SelectListItem
                {
                    Text = "Concern",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.ConcernList = ConcernList;

                LegalOwnerList.Add(new SelectListItem
                {
                    Text = "Legal Owner",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.LegalOwnerList = LegalOwnerList;
            }

            ViewBag.FuelSystemList = lookupFacade.GetLookupByKey("FuelSystem").Select(x =>
                      new SelectListItem()
                      {
                          Text = x.DisplayText.ToString(),
                          Value = x.DataValue.ToString()
                      }).ToList();

            ViewBag.VehicleStatusList = lookupFacade.GetLookupByKey("Status").Select(x =>
                       new SelectListItem()
                       {
                           Text = x.DisplayText.ToString(),
                           Value = x.DataValue.ToString()
                       }).ToList();
            ViewBag.VehicleTypeList = lookupFacade.GetLookupByKey("VehicleType").Select(x =>
                       new SelectListItem()
                       {
                           Text = x.DisplayText.ToString(),
                           Value = x.DataValue.ToString()
                       }).ToList();
            ViewBag.VehicleSubTypeList = lookupFacade.GetLookupByKey("VehicleSubType").Select(x =>
                 new SelectListItem()
                 {
                     Text = x.DisplayText.ToString(),
                     Value = x.DataValue.ToString()
                 }).ToList();
            ViewBag.CapacityList = lookupFacade.GetLookupByKey("Capacity").Select(x =>
            new SelectListItem()
            {
                Text = x.DisplayText.ToString(),
                Value = x.DataValue.ToString()
            }).ToList();
            Car model = new Car();
            if (id.HasValue && id > 0)
            {
                model = carFacade.GetVehicleById(id.Value);

                AllocationList.Add(new SelectListItem
                {
                    Text = model.DriverName,
                    Value = model.DriverId.ToString(),
                    Selected = true

                });
                ViewBag.AllocationList = AllocationList;
                LegalOwnerList.Add(new SelectListItem
                {
                    Text = model.UserName,
                    Value = model.UserId.ToString(),
                    Selected = true
                });
                ViewBag.LegalOwnerList = LegalOwnerList;

                ConcernList.Add(new SelectListItem
                {
                    Text = model.ConcernName,
                    Value = model.CompanyId.ToString()
                });
                ViewBag.ConcernList = ConcernList;


            }



            return View(model);
        }

        public ActionResult AddDriver(Guid? CarId, int? DriverId)
        {
            List<SelectListItem> AllocationList = new List<SelectListItem>();
            List<SelectListItem> LegalOwnerList = new List<SelectListItem>();
            if (CarId != null && CarId != new Guid())
            {
                Car model = carFacade.GetVehicleByCarId(CarId.Value);
                ViewBag.CarId = model.CarId;
            }

            if (DriverId == null)
            {
                AllocationList.Add(new SelectListItem
                {
                    Text = "Driver",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.AllocationList = AllocationList;

                LegalOwnerList.Add(new SelectListItem
                {
                    Text = "Legal Owner",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.LegalOwnerList = LegalOwnerList;
            }

            UserDriverMapVM usermodel = new UserDriverMapVM();
            if (DriverId.HasValue && DriverId > 0)
            {
                usermodel = (UserDriverMapVM)DriverMapFacade.GetDriverMapById(DriverId.Value);

                AllocationList.Add(new SelectListItem
                {
                    Text = usermodel.DriverName,
                    Value = usermodel.DriverId.ToString()
                });
                ViewBag.AllocationList = AllocationList;

            }
            return View(usermodel);
        }

        public JsonResult DeleteAssignDriver(int DriverId)
        {
            bool result = false;
            string message = "";

            try
            {
                DriverMapFacade.Delete(DriverId);
                result = true;
                message = "Driver deleted successfully";

            }
            catch (Exception ex)
            {
                result = false;
                message = "Internal Error!";
            }
            return Json(new { result = result, message = message });

        }

        public JsonResult DeleteDocuments(int DriverId)
        {
            bool result = false;
            string message = "";

            try
            {
                documentsFacade.Delete(DriverId);
                result = true;
                message = "Documents deleted successfully";

            }
            catch (Exception ex)
            {
                result = false;
                message = "Internal Error!";
            }
            return Json(new { result = result, message = message });

        }
        public JsonResult SaveDriver(UserDriverMap userDriver)
        {
            bool result = false;
            string message = "";
            try
            {
                #region Car Driver Map
                if (userDriver.Id > 0)
                {
                    UserDriverMap ump = DriverMapFacade.Get(userDriver.Id);
                    ump.DriverId = userDriver.DriverId;
                    ump.Note = userDriver.Note;
                    DriverMapFacade.Update(ump);
                    result = true;
                    message = "Driver Updated successfully.";
                }
                else
                {
                    if (userDriver.DriverId != null && userDriver.DriverId != new Guid())
                    {
                        DriverMapFacade.Insert(userDriver);
                        result = true;
                        message = "Driver Added successfully.";
                    }
                }

                #endregion
            }
            catch (Exception  ex)
            {
                result = false;
                message = "Internal Error!";
            }
            return Json(new { result = result, message = message });
        }

        public ActionResult LoadAllAssignedDriver(int Id)
        {
            Car model = carFacade.GetVehicleById(Id);
            List<UserDriverMapVM> DriverList = new List<UserDriverMapVM>();
            if (model != null)
            {

                DriverList = DriverMapFacade.GetDriverMapByCarId(model.CarId);


            }
            return View(DriverList);

        }
        public JsonResult SaveVehicle(Car car, UserCarMap userCar, CarConcernMap carConcern)
        {

            var result = false;
            if (car != null)
            {
                if (car.Id > 0)
                {
                    #region car update
                    var updateCar = carFacade.Get(car.Id);

                    car.CreatedDate = updateCar.CreatedDate;


                    if (carFacade.Update(car) > 0)
                    {
                        result = true;
                    }
                    #endregion


                    #region UserCarMap Update
                    UserCarMap carmap = carMapFacade.GetUserCarMapByCarId(updateCar.CarId);
                    if (carmap != null)
                    {
                        carmap.UserId = userCar.UserId;
                        carMapFacade.Update(carmap);
                        result = true;
                    }
                    else
                    {
                        if (userCar.UserId != null && userCar.UserId != new Guid())
                        {
                            userCar.CarId = car.CarId;
                            //userCar.Note = car.Note;
                            carMapFacade.Insert(userCar);
                            result = true;
                        }
                    }

                    #endregion
                }
                else
                {
                    #region add car
                    car.CreatedDate = DateTime.Now;
                    car.CarId = Guid.NewGuid();
                    car.CompanyId = Guid.Empty;

                    if (carFacade.Insert(car) > 0)
                    {
                        result = true;
                    }
                    #endregion

                    #region User Car Map
                    if (userCar.UserId != null && userCar.UserId != new Guid())
                    {
                        userCar.CarId = car.CarId;
                        //userCar.Note = car.Note;
                        carMapFacade.Insert(userCar);
                    }
                    #endregion

                }
            }
            return Json(new { result = result });
        }

        public ActionResult LoadVehicleDocumentList(string CarId)
        {
            List<Documents> documents = new List<Documents>();
            documents = documentsFacade.GetAllByUserId(new Guid(CarId));
            return View(documents);
        }
    }
}