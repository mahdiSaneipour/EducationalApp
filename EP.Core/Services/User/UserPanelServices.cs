using EP.Core.DTOs.UserPanelViewModels;
using EP.Core.Interfaces.User;
using EP.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.User
{
    public class UserPanelServices : IUserPanelServices
    {
        private readonly IUserRepository _userRepository;

        public UserPanelServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserPanelViewModel GetUserInformationForUserPanel(string userId)
        {
            UserPanelViewModel result = new UserPanelViewModel();

            int id = Convert.ToInt32(userId);
            Domain.Entities.User.User user = _userRepository.GetUserByUserId(id);

            result.RegisterDate = user.RegisterDate;
            result.Username = user.UserName;
            result.Email = user.Email;
            result.Wallet = 0;

            return result;
        }
    }
}
