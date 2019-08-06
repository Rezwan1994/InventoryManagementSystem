using SFMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Repository
{
    public class UserLoginRepository: Repository<UserLogin>
    {
        public UserLoginRepository(DataContext dataContext) : base(dataContext) { }
    }
}
