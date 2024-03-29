﻿using EP.Domain.Entities.Permission;
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

        List<int> GetRolesIdByPermissionId(int permissionId);

        int AddUserRole(Domain.Entities.User.UserRole userRole);

        void UpdateUserRoles(int userId, List<UserRole> userRoles);

        void DeleteAllUserRoles(int userId);
        #region Roles
        int AddRole(Role role);

        void RemoveRole(Role role);

        void UpdateRole(Role role);

        Role GetRole(int roleId);

        #endregion

        #region Permissions

        List<Permission> GetAllPermissions();

        void AddRolePermissions(int roleId, List<int> rolePermissions);

        void DeleteRolePermissionsByRoleId(int roleId);

        List<int> GetRolePermissionsIdByRoleId(int roleId);

        #endregion

        void SaveChanges();
    }
}
