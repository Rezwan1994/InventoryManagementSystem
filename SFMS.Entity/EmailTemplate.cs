using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class EmailTemplate:Entity
    {
        public Guid CompanyId { get; set; }
        public string TemplateKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string BccEmail { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ReplyEmail { get; set; }
        public string Subject { get; set; }

        public string BodyContent { get; set; }
        public string BodyFile { get; set; }
        public bool IsActive { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
