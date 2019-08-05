using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
   public class Drivers : Entity
    {
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string ImgSrc { get; set; }
        public string Address { get; set; }
        public string DriverLicense { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public Documents Documents { get; set; }
    }
}
