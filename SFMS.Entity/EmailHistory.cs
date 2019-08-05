using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class EmailHistory:Entity
    {
        public string MyProperty { get; set; }
        public string TemplateKey { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string BccEmail { get; set; }
        public string FromEmail{ get; set; }
        public string FromName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBodyContent { get; set; }
        public DateTime EmailSentDate { get; set; }
        public bool IsSystemAutoSent { get; set; }
        public bool IsRead { get; set; }
        public int ReadCount { get; set; }
        public DateTime LastUpdatedDate { get; set; }

    }
}












