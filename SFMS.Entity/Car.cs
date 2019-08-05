using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class Car : Entity
    {
        public Guid CarId { get; set; }
        public string RegId { get; set; }
     
        public string Model { get; set; }
     
        public string CC { get; set; }
        public string Make { get; set; }
        public string Financier { get; set; }
     
        public Guid CompanyId { get; set; }
        public string FuelSystem { get; set; }
        public string ChassisNo { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Capacity { get; set; }
        public string Note { get; set; }
        public double InitialOdometer { get; set; }
        public double InitialVolume { get; set; }
        public string Status { get; set; }
   
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public string DriverName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string ConcernName { get; set; }
        [NotMapped]
        public Guid DriverId { get; set; }
        [NotMapped]
        public Guid UserId { get; set; }

     
    }
    [NotMapped]
    public class CarVM:Car
    {
     
    }
    [NotMapped]
    public class VehicleFilter
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
        public string Model { get; set; }
        [NotMapped]
        public string Make { get; set; }
        [NotMapped]
        public string RegId { get; set; }
        [NotMapped]
        public string CC { get; set; }
        [NotMapped]
        public string FuelSystem { get; set; }
        [NotMapped]
        public string ChassisNo { get; set; }
        [NotMapped]
        public string VehicleType { get; set; }
        [NotMapped]
        public string VehicleSubType { get; set; }
        [NotMapped]
        public string Capacity { get; set; }
        [NotMapped]
        public string Status { get; set; }
     

    }
    [NotMapped]
   

    public class VehicleModel
    {
        [NotMapped]
        public List<Car> CarList { get; set; }
        [NotMapped]
        public int TotalCount { get; set; }
    }
}
