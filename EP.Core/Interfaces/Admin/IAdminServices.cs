using EP.Core.DTOs.AdminPanelViewModels;
using EP.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.Admin
{
    public interface IAdminServices
    {
        UsersForAdminViewModel GetUsersForAdminServices(int currentPage = 1, string filterEmail = "", string filterUsername = "", bool isDelete = false);

        void DeleteUserByUserId(int userId);

        void AddRolesToUser(List<int> userRoles, int userId);

        void AddRole(CreateRoleViewModel role,List<int> permissions);

        EditRoleViewModel GetRoleByRoleId(int roleId);

        int EditRole(EditRoleViewModel role, List<int> permissions);

        List<Domain.Entities.User.Role> GetAllRoles();

        int CreateUserFromAdmin(CreateUserViewModel model);

        int EditUserFromAdmin(EditUserAdminViewModel model,List<int> userRoles);

        EditUserAdminViewModel GetEditUserViewModel(int userId);

        void RemoveRole(EditRoleViewModel role);
    }
}
