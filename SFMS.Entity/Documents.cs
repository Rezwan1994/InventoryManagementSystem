using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class Documents : Entity
    {
        public Guid DocumentId { get; set; }
        public string DocumentsType { get; set; }
        public string FileSource { get; set; }
        public string LicenseNo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime UploadedDate { get; set; }
        public Guid UserId { get; set; }
    }
}
