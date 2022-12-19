using EP.Domain.Entities.Permission;
using EP.Domain.Entities.User;
using EP.Domain.Interfaces.Roles;
using EP.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public List<Permission> GetAllPermissions()
        {
            return _context.Permissions.ToList();
        }

        public void DeleteRolePermissionsByRoleId(int roleId)
        {
            _context.RolePermissions.Where(r => r.RoleId == roleId).ToList()
                .ForEach(rolePermission =>
                {
                    _context.Remove(rolePermission);
                });
        }

        public void AddRolePermissions(int roleId, List<int> rolePermissions)
        {
            foreach(int rolePermission in rolePermissions)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    PermissionId = rolePermission,
                    RoleId = roleId
                });
            }
        }
        public List<int> GetRolePermissionsIdByRoleId(int roleId)
        {
            return _context.RolePermissions.Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToList();
        }

        public List<int> GetRolesIdByPermissionId(int permissionId)
        {
            return _context.RolePermissions.Where(r => r.PermissionId == permissionId)
                .Select(r => r.RoleId).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
