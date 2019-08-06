using SFMS.Facade;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.Data;
using System.IO;

using ClosedXML.Excel;
using SFMS.Framework;
using NsExcel = Microsoft.Office.Interop.Excel;

using Rotativa.Options;
using SFMS.Entity;
using System.ComponentModel;

namespace SmartFleetManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        LookUpFacade lookupFacade = null;
        CarFacade carFacade = null;
        FuelBillFacade fuelbillfacade = null;
        public ReportController()
        {
            DataContext Context = DataContext.getInstance();
            lookupFacade = new LookUpFacade(Context);
            carFacade = new CarFacade(Context);
            fuelbillfacade = new FuelBillFacade(Context);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReportsPartial()
        {
          
            return View();
        }

        public ActionResult VehicleFilter()
        {
            #region ViewBags
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
            #endregion
            return View();
        }
        public ActionResult FuelFilter()
        {
            List<SelectListItem> CarList = new List<SelectListItem>();
            #region ViewBags
            CarList.Add(new SelectListItem
            {
                Text = "Vehicle",
                Value = "00000000-0000-0000-0000-000000000000"
            });
            ViewBag.CarList = CarList;

            ViewBag.FuelSystemList = lookupFacade.GetLookupByKey("FuelSystem").Select(x =>
                    new SelectListItem()
                    {
                        Text = x.DisplayText.ToString(),
                        Value = x.DataValue.ToString(),
                    }).ToList();
            #endregion
            return View();
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

        public FileResult NewReport(string ColumnNames, string ReportFor, string NumberPrefix, string SelectAllIds, string activeOrinactive, string UserList, string ExportType, string FilterUser, string isSelected)
        {

            List<string> IdList = new List<string>();
            string[] Users = null;
            if (ExportType == "2")
            {
                Users = FilterUser.Split(',');
            }
            if (ExportType == "3")
            {
                Users = UserList.Split(',');
            }
            string[] ColumnList = null;

            if (ReportFor == "Vehicle")
            {
                //foreach (var item in idList) 
             
                if (SelectAllIds == "filtered")
                {
                    IdList = (List<string>)System.Web.HttpRuntime.Cache["GetAllVehicleIdList"];

                }
                else
                {
                    List<Product> idList = carFacade.GetAll();
                    foreach (var item in idList)
                    {
                        IdList.Add(item.Id.ToString());
                    }
                }
            }
            if (ReportFor == "Fuel")
            {
                //foreach (var item in idList) 

                if (SelectAllIds == "filtered")
                {
                    IdList = (List<string>)System.Web.HttpRuntime.Cache["GetAllFuelBillIdList"];

                }
                else
                {
                    List<PurchaseOrder> idList = fuelbillfacade.GetAll();
                    foreach (var item in idList)
                    {
                        IdList.Add(item.Id.ToString());
                    }
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dtResult = new DataTable();
                
                if (!string.IsNullOrWhiteSpace(ReportFor) && ReportFor == "Vehicle")
                {
                    dtResult = ToDataTable(carFacade.GetAllVehiclesbyIdList(IdList));
                    dtResult.TableName = "Vehicle";
                    dtResult.Columns.Remove("CarId");
                    dtResult.Columns.Remove("CompanyId");
                    dtResult.Columns.Remove("DriverId");
                    dtResult.Columns.Remove("UserId");
                    dtResult.Columns.Remove("Id");
                }
                else if (!string.IsNullOrWhiteSpace(ReportFor) && ReportFor == "Fuel")
                {
                    dtResult = ToDataTable(fuelbillfacade.GetAllFuelBillbyIdList(IdList));
                    dtResult.TableName = "Fuel";
                
                }
                else
                {
                   // dtResult = _Util.Facade.CustomerFacade.GetCustomerReport(IdList.ToArray(), ColumnList, CurrentUser.CompanyId.Value, null, activeOrinactive);
                }
                //base.AddPageSubmitUserActivity("Download " + ReportFor + " report" + LabelHelper.ActivityAction.Success);
                if (dtResult != null)
                {
                
                    var ws = wb.Worksheets.Add(dtResult);
               
         
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;

                    MemoryStream memorystreem = new MemoryStream();
                    wb.SaveAs(memorystreem);
                    var fName = string.Format("{0}-{1}.xlsx", ReportFor + "Report", DateTime.Now.UTCCurrentTime().ToString("s"));

                    byte[] fileContents = memorystreem.ToArray();
                    return File(new MemoryStream(fileContents, 0, fileContents.Length), "application/octet-stream", fName);


                }
                else
                {
                    byte[] fileContents = new byte[1];
                               return File(new MemoryStream(fileContents, 0, fileContents.Length), "application/octet-stream", $"dmpty.xlsx");
                }
            }
        }

    }
}