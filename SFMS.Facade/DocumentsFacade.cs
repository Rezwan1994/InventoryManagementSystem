using SFMS.Entity;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class DocumentsFacade : Facade<Documents>
    {
        DocumentsRepository documentsRepository = null;
        public DocumentsFacade(DataContext dataContext) : base(dataContext) {
            documentsRepository = new DocumentsRepository(dataContext);
        }

        public Documents GetByUserId(Guid UserId)
        {
            return documentsRepository.GetByUserId(UserId);
        }

        public List<Documents> GetAllByUserId(Guid UserId)
        {
            return documentsRepository.GetAllByUserId(UserId);
        }
    }
}
