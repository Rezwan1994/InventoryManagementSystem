using SFMS.Framework;
using SmartFleetManagementSystem.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace HS.Web.UI.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NoFile()
        {
            return View();
        }

        //public ActionResult Download()
        //{

        //    if (Request.QueryString.Count == 0)
        //    {
        //        return RedirectPermanent("/");
        //    }
        //    else
        //    {
        //        string Idstr = DESEncryptionDecryption.DecryptCipherTextToPlainText((HttpUtility.UrlDecode(Request.QueryString[0].ToString())));

        //        CustomerFile filename = _Util.Facade.CustomerFileFacade.GetFileNameById(Int32.Parse(Idstr));
        //        // string Filenum = Encryption.Encrypt(filename.Id.ToString(),true);
        //        if (filename.FileFullName == "")
        //        {
        //            filename.FileFullName = filename.FileDescription;
        //        }
        //        try
        //        {
        //            string fullName = Server.MapPath("~" + filename.Filename);
        //            if (!System.IO.File.Exists(fullName))
        //            {
        //                return PartialView("~/Views/Shared/_FileNotFound.cshtml");
        //            }

        //            byte[] fileBytes = System.IO.File.ReadAllBytes(fullName);
        //            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename.FileFullName);
        //        }
        //        catch (Exception)
        //        {
        //            return PartialView("~/Views/Shared/_FileNotFound.cshtml");
        //        }

        //    }
        //}

        //public ActionResult UserFileDownload()
        //{

        //    if (Request.QueryString.Count == 0)
        //    {
        //        return RedirectPermanent("/");
        //    }
        //    else
        //    {
        //        string Idstr = DESEncryptionDecryption.DecryptCipherTextToPlainText( (HttpUtility.UrlDecode(Request.QueryString[0].ToString())));

        //        HrDoc filename = _Util.Facade.HrDocFacade.GetFileNameById(Int32.Parse(Idstr));
        //        // string Filenum = Encryption.Encrypt(filename.Id.ToString(),true);
        //        string fullName = "";
        //        try {
        //            fullName = Server.MapPath("~" + filename.Filename);

        //            if (!System.IO.File.Exists(fullName))
        //            {
        //                return PartialView("~/Views/Shared/_FileNotFound.cshtml");
        //                //return Json(new { result = "File not exists" }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return PartialView("~/Views/Shared/_FileNotFound.cshtml");
        //            //return Json(new { result = "File not exists" },JsonRequestBehavior.AllowGet);
        //        }

        //        string name = filename.Filename;

        //        if (filename.FileDescription.Length < 60)
        //        {
        //            var fileformat = filename.Filename.Split('.');
        //            if (fileformat.Length == 2)
        //            {
        //                name =  string.Concat(filename.FileDescription ,".", fileformat[1]) ;
        //            }
        //        }

        //        byte[] fileBytes = System.IO.File.ReadAllBytes(fullName);
        //        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        //    }
        //}


        //[Authorize]
        //[HttpPost]
        //public JsonResult DeleteCustomerFile(int Id, int DriverId)
        //{
        //    //var CurrentUser = (HS.Web.UI.Helper.CustomPrincipal)User;
        //    bool res = _Util.Facade.CustomerFacade.CustomerIsInCompany(CustomerId, CurrentUser.CompanyId.Value);
        //    if (!res)
        //    {
        //        return Json(new { result = false, message = "Invalid Customer" });
        //    }
        //    Customer tmpCustomer = _Util.Facade.CustomerFacade.GetById(CustomerId);
        //    CustomerFile tmpCF = _Util.Facade.CustomerFileFacade.GetAllFilesByCustomerIdAndCompanyId(Id);
        //    if (tmpCF.CustomerId != tmpCustomer.CustomerId)
        //    {
        //        return Json(new { result = false, message = "Invalid Customer" });
        //    }

        //    var serverFile = Server.MapPath(tmpCF.Filename);
        //    if (System.IO.File.Exists(serverFile))
        //    {
        //        System.IO.File.Delete(serverFile);
        //    }
        //    _Util.Facade.CustomerFileFacade.DeleteById(Id);

        //    return Json(new { result = true, message = "File deleted successfully." });
        //}

        //[Authorize]
        public ActionResult UploadDrivingLicense()
        {
            bool isUploaded = false;
            string exception = "";
            HttpPostedFileBase httpPostedFileBase = Request.Files["DrivingLicense"];

            string tempFolderName = ConfigurationManager.AppSettings["File.DrivingLicense"];

            tempFolderName = string.Format(tempFolderName, DateTime.Now.ToString("MM-dd-yy"));

            Random rand = new Random();
            string FileName = rand.Next().ToString();
            FileName += httpPostedFileBase.FileName;

            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength != 0)
            {

                string tempFolderPath = Server.MapPath("~/" + tempFolderName);

                if (FileHelper.CreateFolderIfNeeded(tempFolderPath) == "1")
                {
                    try
                    {
                        httpPostedFileBase.SaveAs(Path.Combine("/", tempFolderPath, FileName));
                        isUploaded = true;
                    }
                    catch (Exception) {  /*TODO: You must process this exception.*/}
                }
                else
                {
                    exception = FileHelper.CreateFolderIfNeeded(tempFolderPath);
                }
            }
            string FullFilePath = "";
            string filePath = string.Concat(tempFolderName, FileName);
            FullFilePath = ConfigurationManager.AppSettings["SiteDomain"] + filePath;
            return Json(new { isUploaded = isUploaded, filePath = filePath, FullFilePath = FullFilePath, exception = exception }, "text/html");
        }

        public ActionResult UploadProductDocuments()
        {
            bool isUploaded = false;
            HttpPostedFileBase httpPostedFileBase = Request.Files["UploadFuelDocuments"];

            string tempFolderName = ConfigurationManager.AppSettings["File.UploadProductDocuments"];

            tempFolderName = string.Format(tempFolderName, DateTime.Now.ToString("MM-dd-yy"));

            Random rand = new Random();
            string FileName = rand.Next().ToString();
            FileName += httpPostedFileBase.FileName;

            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength != 0)
            {

                string tempFolderPath = Server.MapPath("~/" + tempFolderName);

                if (FileHelper.CreateFolderIfNeeded(tempFolderPath) == "1")
                {
                    try
                    {
                        httpPostedFileBase.SaveAs(Path.Combine("/", tempFolderPath, FileName));
                        isUploaded = true;
                    }
                    catch (Exception) {  /*TODO: You must process this exception.*/}
                }
            }
            string FullFilePath = "";
            string filePath = string.Concat(tempFolderName, FileName);
            FullFilePath = ConfigurationManager.AppSettings["SiteDomain"] + filePath;
            return Json(new { isUploaded = isUploaded, filePath = filePath, FullFilePath = FullFilePath }, "text/html");
        }

        public ActionResult UploadVehicleDocuments(string type)
        {
            bool isUploaded = false;
            HttpPostedFileBase httpPostedFileBase = Request.Files["VehicleDocuments"];

            string tempFolderName = ConfigurationManager.AppSettings["File.VehicleDocuments"];
            type = type.Replace(' ', '_');
            tempFolderName = string.Format(tempFolderName, DateTime.Now.ToString("MM-dd-yy"),type);

            Random rand = new Random();
            string FileName = rand.Next().ToString();
            FileName += httpPostedFileBase.FileName;

            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength != 0)
            {

                string tempFolderPath = Server.MapPath("~/" + tempFolderName);

                if (FileHelper.CreateFolderIfNeeded(tempFolderPath) == "1")
                {
                    try
                    {
                        httpPostedFileBase.SaveAs(Path.Combine("/", tempFolderPath, FileName));
                        isUploaded = true;
                    }
                    catch (Exception) {  /*TODO: You must process this exception.*/}
                }
            }
            string FullFilePath = "";
            string filePath = string.Concat(tempFolderName, FileName);
            FullFilePath = ConfigurationManager.AppSettings["SiteDomain"] + filePath;
            return Json(new { isUploaded = isUploaded, filePath = filePath, FullFilePath = FullFilePath }, "text/html");
        }
        //[Authorize]
        public PartialViewResult AddFile(int Id)
        {
            ViewBag.CustomerId = Id;
            return PartialView("_AddFile");
        }
    }
}