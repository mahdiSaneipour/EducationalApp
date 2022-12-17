using EP.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.Roles
{
    public interface IRoleRepository
    {
        List<Domain.Entities.User.Role> GetAllRoles();

        List<int> GetUserRolesId(int userId);

        int AddUserRole(Domain.Entities.User.UserRole userRole);

        void UpdateUserRoles(int userId, List<UserRole> userRoles);

        void DeleteAllUserRoles(int userId);

        void SaveChanges();
    }
}
