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
        public int AddRole(Role role)
        {
            _context.Roles.Add(role);

            return role.RoleId;
        }

        public void RemoveRole(Role role)
        {
            _context.Roles.Remove(role);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
        }

        public Role GetRole(int roleId)
        {
            return _context.Roles.First(r => r.RoleId == roleId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
