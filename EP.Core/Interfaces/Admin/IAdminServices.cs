using EP.Core.DTOs.AdminPanelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Admin
{
    public interface IAdminServices
    {
        UsersForAdminViewModel GetUsersForAdminServices(int currentPage = 1, string filterEmail = "", string filterUsername = "");

        void AddRolesToUser(List<int> userRoles, int userId);

        List<Domain.Entities.User.Role> GetAllRoles();

        int CreateUserFromAdmin(CreateUserViewModel model);

        int EditUserFromAdmin(EditUserAdminViewModel model,List<int> userRoles);

        EditUserAdminViewModel GetEditUserViewModel(int userId);
    }
}
