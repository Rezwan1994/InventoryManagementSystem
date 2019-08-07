using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSRepository
{
    public class EmailHistoryRepository : Repository<EmailHistory>
    {
        public EmailHistoryRepository(DataContext dataContext) : base(dataContext) { }
    }
}
