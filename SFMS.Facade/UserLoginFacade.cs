using SFMS.Entity;
using IMSRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMS.Facade
{
    public class UserLoginFacade : Facade<UserLogin>
    {
        public UserLoginFacade(DataContext dataContext) : base(dataContext) { }
    }
}
