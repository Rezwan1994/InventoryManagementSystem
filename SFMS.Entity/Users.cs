using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class Users : Entity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ImgSrc { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

    }
  
}
