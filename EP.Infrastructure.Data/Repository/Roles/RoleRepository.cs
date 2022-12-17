using EP.Domain.Entities.User;
using EP.Domain.Interfaces.Roles;
using EP.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EPContext _context;

        public RoleRepository(EPContext context)
        {
            _context = context;
        }

        public int AddUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            return userRole.UR_Id;
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public List<int> GetUserRolesId(int userId)
        {
            return _context.UserRoles.Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();
        }

        public void DeleteAllUserRoles(int userId)
        {
            _context.UserRoles.Where(r => r.UserId == userId).ToList()
                .ForEach(r => _context.UserRoles.Remove(r));
        }

        public void UpdateUserRoles(int userId, List<UserRole> userRoles)
        {
            DeleteAllUserRoles(userId);

            _context.UserRoles.AddRange(userRoles);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
