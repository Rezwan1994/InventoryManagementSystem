using IMSRepository;
using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class PaymentFacade : Facade<PaymentReceive>
    {
        PaymentRepository PaymentRepository = null;
        public PaymentFacade(DataContext dataContext) : base(dataContext)
        {
            PaymentRepository = new PaymentRepository(dataContext);
        }
       

        public List<PaymentReceive> GetAllPaymentReceiveByCustomerId(Guid CustomerId)
        {
            return PaymentRepository.GetAllPaymentReceiveByCustomerId(CustomerId);
        }
    }
}
