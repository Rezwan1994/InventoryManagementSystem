using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
