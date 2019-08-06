using SFMS.Entity;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class DocumentsFacade : Facade<SalesOrderDetail>
    {
        DocumentsRepository documentsRepository = null;
        public DocumentsFacade(DataContext dataContext) : base(dataContext) {
            documentsRepository = new DocumentsRepository(dataContext);
        }

        public SalesOrderDetail GetByUserId(Guid UserId)
        {
            return documentsRepository.GetByUserId(UserId);
        }

        public List<SalesOrderDetail> GetAllByUserId(Guid UserId)
        {
            return documentsRepository.GetAllByUserId(UserId);
        }
    }
}
