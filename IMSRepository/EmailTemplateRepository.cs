using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSRepository
{
    class EmailTemplateRepository: Repository<EmailTemplate>
    {
        public EmailTemplateRepository(DataContext dataContext) : base(dataContext) { }
    }
}
