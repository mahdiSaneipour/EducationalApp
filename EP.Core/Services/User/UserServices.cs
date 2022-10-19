using EP.Core.DTOs.AccountViewModels;
using EP.Core.Interfaces.User;
using EP.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsEmailExist(string email)
        {
            return _userRepository.IsEmailExist(email);
        }

        public bool IsUserNameExist(string username)
        {
            return _userRepository.IsUserNameExist(username);
        }

        public int AddUser(RegisterUserViewModel register)
        {
            EP.Domain.Entities.User.User user = new Domain.Entities.User.User()
            {
                Email = register.Email,
                UserName = register.UserName,
                
            };



            throw new NotImplementedException();
        }
    }
}
