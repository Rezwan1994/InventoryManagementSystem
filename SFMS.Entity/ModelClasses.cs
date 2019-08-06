using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFMS.Entity
{
    public class ModelClasses
    {
    }
    public class UsersFilter
    {
        public int? UnitPerPage { get; set; }
        public int? PageNumber { get; set; }
        public string Order { get; set; }
        public string SearchText { get; set; }
        public string Type { get; set; }
        public string status { get; set; }
    }
    public class UsersModel
    {
        public List<Users> UsersList { get; set; }
        public int TotalCount { get; set; }
    }
    public class DriversModel
    {
        public List<SalesOrder> DriversList { get; set; }
        public int TotalCount { get; set; }
    }
    public class DriversFilter
    {
        public int? UnitPerPage { get; set; }
        public int? PageNumber { get; set; }
        public string Order { get; set; }
        public string SearchText { get; set; }
        public string Type { get; set; }
        public string status { get; set; }
    }

    public class DriverInfo
    {
        public SalesOrder Drivers { get; set; }
        public SalesOrderDetail Documents { get; set; }
    }

    public static class DocumentType
    {
        public static string DrivingLicense { get{ return "Driving License"; }}
    }
}