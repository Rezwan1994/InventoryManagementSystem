using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SFMS.Repository
{
    public class DocumentsRepository : Repository<SalesOrderDetail>
    {
        DataContext context = null;
        public DocumentsRepository(DataContext dataContext) : base(dataContext) {
            this.context = dataContext;
        }

        public SalesOrderDetail GetByUserId(Guid UserId)
        {
            return context.Documents.SqlQuery($"Select *from Documents where UserId = '{UserId}'").FirstOrDefault();
        }

        public List<SalesOrderDetail> GetAllByUserId(Guid UserId)
        {
            return context.Documents.SqlQuery($"Select *from Documents where UserId = '{UserId}'").ToList();
        }
    }
}
