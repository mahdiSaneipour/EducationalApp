using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.Admin
{
    public class AdminServices : IAdminServices
    {
        private readonly IUserRepository _userRepository;

        public AdminServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UsersForAdminViewModel GetUsersForAdminServices(int currentPage = 1, string filterEmail = "", string filterUsername = "")
        {
            int take = 10;
            int skip = (currentPage - 1) * take;

            List<Domain.Entities.User.User> users = new List<Domain.Entities.User.User>();
            IQueryable<Domain.Entities.User.User> qUsers = _userRepository.GetUsersForAdmin(filterEmail,filterUsername);

            int countPage = (qUsers.Count() / take);

            users = qUsers.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            UsersForAdminViewModel result = new UsersForAdminViewModel()
            {
                CurrentId = currentPage,
                PageCount = countPage,
                Users = users
            };

            return result;
        }
    }
}
