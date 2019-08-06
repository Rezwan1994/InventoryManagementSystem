using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Repository
{
    class EmailTemplateRepository: Repository<EmailTemplate>
    {
        public EmailTemplateRepository(DataContext dataContext) : base(dataContext) { }
    }
}
