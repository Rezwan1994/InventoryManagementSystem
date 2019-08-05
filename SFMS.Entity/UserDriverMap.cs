using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class UserDriverMap : Entity
    {
        public Guid DriverId { get; set; }
        public Guid CarId { get; set; }
        public string Note { get; set; }
    }
    [NotMapped]
    public class UserDriverMapVM : UserDriverMap
    {
        public string DriverName { get; set; }
        public string Mobile { get; set; }
    }
}
