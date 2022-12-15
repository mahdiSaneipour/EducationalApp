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

        int AddUserRole(Domain.Entities.User.UserRole userRole);

        void SaveChanges();
    }
}
