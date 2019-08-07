using SFMS.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSRepository;

namespace SFMS.Facade
{
    public class UserFacade : Facade<Users>
    {
        UserRepository userRepository = null;
        public UserFacade(DataContext dataContext) : base(dataContext) {
            userRepository = new UserRepository(dataContext);
        }
        public UsersModel GetUsers(UsersFilter filter)
        {
            return userRepository.GetUsers(filter);
        }
    }
}
