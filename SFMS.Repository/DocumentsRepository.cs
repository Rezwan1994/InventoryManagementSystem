using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFMS.Repository
{
    public class DocumentsRepository : Repository<Documents>
    {
        DataContext context = null;
        public DocumentsRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public Documents GetByUserId(Guid UserId)
        {
            return context.Documents.SqlQuery($"Select *from Documents where UserId = '{UserId}'").FirstOrDefault();
        }

        public List<Documents> GetAllByUserId(Guid UserId)
        {
            return context.Documents.SqlQuery($"Select *from Documents where UserId = '{UserId}'").ToList();
        }
    }
}
