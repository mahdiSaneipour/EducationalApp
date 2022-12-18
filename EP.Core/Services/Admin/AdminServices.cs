using EP.Core.DTOs.AdminPanelViewModels;
using EP.Core.Interfaces.Admin;
using EP.Domain.Entities.User;
using EP.Domain.Interfaces.Roles;
using EP.Domain.Interfaces.User;
using Microsoft.AspNetCore.Localization;
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
        private readonly IRoleRepository _roleRepository;

        public AdminServices(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public UsersForAdminViewModel GetUsersForAdminServices(int currentPage = 1, string filterEmail = "", string filterUsername = "", bool isDelete = false)
        {
            int take = 10;
            int skip = (currentPage - 1) * take;

            List<Domain.Entities.User.User> users = new List<Domain.Entities.User.User>();
            IQueryable<Domain.Entities.User.User> qUsers = _userRepository.GetUsersForAdmin(filterEmail,filterUsername,isDelete);

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

        public int CreateUserFromAdmin(CreateUserViewModel model)
        {

            

            Domain.Entities.User.User user = new Domain.Entities.User.User()
            {
                Password = Tools.Security.PasswordHelper.EncodePasswordMd5(model.Password),
                UserCode = Tools.Generator.NameGenerator.GenerateUniqCode(),
                UserAvatar = model.SelectedAvatar,
                RegisterDate = DateTime.Now,
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };

            _userRepository.AddUser(user);

            _userRepository.SaveChanges();

            int userId = user.UserId;

            return userId;
        }

        public void AddRolesToUser(List<int> userRoles, int userId)
        {

            foreach (var userRole in userRoles)
            {
                _roleRepository.AddUserRole(new UserRole()
                {
                    RoleId = userRole,
                    UserId = userId
                });
            }

            _roleRepository.SaveChanges();
        }

        public List<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public EditUserAdminViewModel GetEditUserViewModel(int userId)
        {

            Domain.Entities.User.User user = _userRepository.GetUserByUserId(userId);

            EditUserAdminViewModel model = new EditUserAdminViewModel()
            {
                Email = user.Email,
                userId = user.UserId,
                Username = user.UserName,
                SelectedAvatar = user.UserAvatar
            };

            model.UserRoles = _roleRepository.GetUserRolesId(userId);

            return model;
        }

        public int EditUserFromAdmin(EditUserAdminViewModel model, List<int> userRoles)
        {
            Console.WriteLine("user avatar : " + model.SelectedAvatar);
            Domain.Entities.User.User user = _userRepository.GetUserByUserId(model.userId);

            user.UserName = model.Username;
            user.UserAvatar = model.SelectedAvatar;

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = Tools.Security.PasswordHelper.EncodePasswordMd5(model.Password);
            }

            List<UserRole> roles = new List<UserRole>();

            foreach (var roleId in userRoles)
            {
                roles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = user.UserId
                });
            }

            _roleRepository.UpdateUserRoles(user.UserId,roles);
            _roleRepository.SaveChanges();

            return user.UserId;
        }

        public void DeleteUserByUserId(int userId)
        {
            Domain.Entities.User.User user = _userRepository.GetUserByUserId(userId);

            user.IsDelete = true;

            _userRepository.SaveChanges();
        }

        public void AddRole(CreateRoleViewModel role, List<int> permissions)
        {

            Role newRole = new Role()
            {
                RoleName = role.roleName
            };

            _roleRepository.AddRole(newRole);
            _roleRepository.SaveChanges();
            
            // Add Permission
        }

        public EditRoleViewModel GetRoleByRoleId(int roleId)
        {
            Role role = _roleRepository.GetRole(roleId);

            EditRoleViewModel result = new EditRoleViewModel()
            {
                roleId = role.RoleId,
                roleName = role.RoleName
            };

            return result;
        }

        public int EditRole(EditRoleViewModel role, List<int> permissions)
        {

            Role editRole = _roleRepository.GetRole(role.roleId);

            editRole.RoleName = role.roleName;

            _roleRepository.UpdateRole(editRole);

            // Edit permissions

            _roleRepository.SaveChanges();

            return editRole.RoleId;
        }

        public void RemoveRole(EditRoleViewModel role)
        {
            Role deleteRole = new Role() {
                RoleName = role.roleName,
                RoleId = role.roleId
            };

            _roleRepository.RemoveRole(deleteRole);
            _roleRepository.SaveChanges();
        }
    }
}
