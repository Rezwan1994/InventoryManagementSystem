using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Entity
{
    public class LookUp:Entity
    {
        public string ParentDataKey { get; set; }
        public string DataKey { get; set; }
        public string DisplayText { get; set; }
        public string DataValue { get; set; }
        public int DataOrder { get; set; }
        public bool IsActive { get; set; }
        public string AlterDisplayText { get; set; }
        public string AlterDisplayText1 { get; set; }

    }
}
