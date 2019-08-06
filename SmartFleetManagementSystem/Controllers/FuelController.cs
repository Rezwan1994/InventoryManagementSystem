using SFMS.Entity;
using SFMS.Facade;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SmartFleetManagementSystem.Controllers
{
    public class FuelController : Controller
    {
        FuelBillFacade FuelFacade = null;
        LookUpFacade lookupFacade = null;
        CarFacade carFacade = null;
        public FuelController()
        {
            DataContext Context = DataContext.getInstance();
            FuelFacade = new FuelBillFacade(Context);
            lookupFacade = new LookUpFacade(Context);
            carFacade = new CarFacade(Context);
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
        public ActionResult FuelSystemList()
        {
            var FuelBillList = FuelFacade.GetAll();
            ViewBag.TotalFuelCost = 0;
            ViewBag.FuelCostForOctane = 0;
            ViewBag.FuelCostForDiesel = 0;
            ViewBag.FuelCostForGas = 0;
            if (FuelBillList.Count() > 0)
            {
                ViewBag.TotalFuelCost = FuelBillList.Sum(x => x.TotalCost);
                var FuelCostForOctane  = FuelBillList.Where(x => x.FuelSystem == "Octane");
                if (FuelCostForOctane.Count() > 0)
                {
                    ViewBag.FuelCostForOctane = FuelCostForOctane.Sum(x => x.TotalCost);
                }
                var FuelCostForDiesel = FuelBillList.Where(x => x.FuelSystem == "Diesel");
                if (FuelCostForDiesel.Count() > 0)
                {
                    ViewBag.FuelCostForDiesel = FuelCostForDiesel.Sum(x => x.TotalCost);
                }
                var FuelCostForGas = FuelBillList.Where(x => x.FuelSystem == "Compressed Natural Gas (CNG)");
                if (FuelCostForGas.Count() > 0)
                {
                    ViewBag.FuelCostForGas = FuelCostForGas.Sum(x => x.TotalCost);
                }
            }
            return View();
        }
        public ActionResult LoadFuelSystemList(FuelFilter filter)
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
           FuelModel FuelList = FuelFacade.GetFuels(filter);
            List<string> idList = new List<string>();
            foreach (var item in FuelList.FuelList)
            {
                idList.Add(item.Id.ToString());
            }
            //Need to change
            System.Web.HttpRuntime.Cache["GetAllFuelBillIdList"] = idList;
            ViewBag.OutOfNumber = FuelList.TotalCount;
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



            return View(FuelList.FuelList);
        }
      
        public JsonResult SaveDailyFuel(PurchaseOrder fuelbill, string CarId)
        {
            bool result = false;
            string message = "";
            try
            {
                #region Car Driver Map
                if (fuelbill.Id > 0)
                {
                    PurchaseOrder ump = FuelFacade.Get(fuelbill.Id);
                    ump.CarId = fuelbill.CarId;
                    ump.DriverId = fuelbill.DriverId;
                    ump.FuelSystem = fuelbill.FuelSystem;
                    ump.Usage = fuelbill.Usage;
                    ump.UnitPrice = fuelbill.UnitPrice;
                    ump.FuelAmount = fuelbill.FuelAmount;
                    ump.IssueDate = fuelbill.IssueDate;
                    ump.Odometer = fuelbill.Odometer;
                    ump.LastReading = fuelbill.LastReading;
                    ump.DocumentSrc = fuelbill.DocumentSrc;
                    ump.VoucherNo = fuelbill.VoucherNo;
                    ump.TotalCost = fuelbill.FuelAmount * fuelbill.UnitPrice;
                    FuelFacade.Update(ump);
                    result = true;
                    message = "Fuel bill Updated successfully.";
                }
                else
                {
                    fuelbill.FuelId = Guid.NewGuid();
                    fuelbill.TotalCost = fuelbill.FuelAmount * fuelbill.UnitPrice;
                    fuelbill.IssueDate = Convert.ToDateTime(fuelbill.IssueDate);
                    FuelFacade.Insert(fuelbill);
                    result = true;
                    message = "Fuel bill added successfully.";
                }
                Product cars = carFacade.GetVehicleByCarId(fuelbill.CarId);
                cars.InitialOdometer = fuelbill.Odometer;
                cars.InitialVolume = fuelbill.FuelAmount;
                carFacade.Update(cars);
                #endregion
            }
            catch (Exception ex)
            {
                result = false;
                message = "Internal Error!";
            }
            return Json(new { result = result, message = message });
        }
        public ActionResult AddDailyFuel(int? id)
        {
            List<SelectListItem> DriverList = new List<SelectListItem>();
            List<SelectListItem> CarList = new List<SelectListItem>();

            if (id == null)
            {
                DriverList.Add(new SelectListItem
                {
                    Text = "Driver",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.DriverList = DriverList;
                CarList.Add(new SelectListItem
                {
                    Text = "Vehicle",
                    Value = "00000000-0000-0000-0000-000000000000"
                });
                ViewBag.CarList = CarList;
            }

            FuelBillVM fuel = new FuelBillVM();
            ViewBag.FuelSystemList = lookupFacade.GetLookupByKey("FuelSystem").Select(x =>
                    new SelectListItem()
                    {
                        Text = x.DisplayText.ToString(),
                        Value = x.DataValue.ToString(),
                    }).ToList();
            if (id.HasValue && id > 0)
            {
                 fuel  = FuelFacade.GetFuelsById(id.Value);

                DriverList.Add(new SelectListItem
                {
                    Text = fuel.DriverName,
                    Value = fuel.DriverId.ToString()
                });
                ViewBag.DriverList = DriverList;
                CarList.Add(new SelectListItem
                {
                    Text = fuel.CarName,
                    Value = fuel.CarId.ToString()
                });
                ViewBag.CarList = CarList;

            }
            return View(fuel);
        }
    }
}