using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity 
{
    public class CarConcernMap : Entity
    {
        public Guid ConcernId { get; set; }
        public Guid CarId { get; set; }
        public string Note { get; set; }
    }
}
