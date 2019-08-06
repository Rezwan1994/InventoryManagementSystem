using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class Concerns : Entity
    {
        public Guid ConcernId { get; set; }
        public string ConcernName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string WebSite { get; set; }
        public string ConcernType { get; set; }
        public string Note { get; set; }
        public string ConcernLogo { get; set; }
    }
    [NotMapped]
    public class ConcernFilter
    {
        [NotMapped]
        public int? UnitPerPage { get; set; }
        [NotMapped]
        public int? PageNumber { get; set; }
        [NotMapped]
        public string Order { get; set; }
        [NotMapped]
        public string SearchText { get; set; }

    }
    [NotMapped]
    public class ConcernModel
    {
        [NotMapped]
        public List<Concerns> ConcernsList { get; set; }
        [NotMapped]
        public int TotalCount { get; set; }
    }
}
