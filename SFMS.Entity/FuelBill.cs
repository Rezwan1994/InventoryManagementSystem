using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class FuelBill : Entity
    {
        public Guid FuelId { get; set; }
        public Guid CarId { get; set; }
        public Guid DriverId { get; set; }
        public string FuelSystem { get; set; }
        public double LastReading { get; set; }
        public double Odometer { get; set; }
        public double Usage { get; set; }
        public double FuelAmount { get; set; }
        public double UnitPrice { get; set; }
        public double TotalCost { get; set; }
        public DateTime IssueDate { get; set; }
        public string DocumentSrc { get; set; }
        public string VoucherNo { get; set; }
    }

    [NotMapped]
    public class FuelBillVM : FuelBill
    {
        public string CarName { get; set; }
        public string DriverName { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
    [NotMapped]
    public class FuelFilter
    {
        [NotMapped]
        public int? UnitPerPage { get; set; }
        [NotMapped]
        public int? PageNumber { get; set; }
        [NotMapped]
        public string Order { get; set; }
        [NotMapped]
        public string SearchText { get; set; }
        [NotMapped]
        public Guid Make { get; set; }
        [NotMapped]
        public string FuelSystem { get; set; }
        [NotMapped]
        public string LastReading { get; set; }
        [NotMapped]
        public string Odometer { get; set; }
        [NotMapped]
        public string Usage { get; set; }
        [NotMapped]
        public DateTime IssueDate { get; set; }
        [NotMapped]
        public string VoucharNo { get; set; }
   

    }
    [NotMapped]
    public class FuelModel
    {
        [NotMapped]
        public List<FuelBillVM> FuelList { get; set; }
        [NotMapped]
        public int TotalCount { get; set; }
    }
}
