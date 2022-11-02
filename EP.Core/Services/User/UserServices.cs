using EP.Core.DTOs.AccountViewModels;
using EP.Core.Interfaces.User;
using EP.Core.ServiceModels.Account;
using EP.Core.Tools.FixTexts;
using EP.Core.Tools.Generator;
using EP.Core.Tools.Security;
using EP.Domain.Entities.User;
using EP.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsEmailExist(string email)
        {
            return _userRepository.IsEmailExist(email);
        }

        public bool IsUserNameExist(string username)
        {
            return _userRepository.IsUserNameExist(username);
        }

        public int AddUser(RegisterUserViewModel register)
        {
            EP.Domain.Entities.User.User user = new Domain.Entities.User.User()
            {
                Email = register.Email,
                UserName = register.UserName,
                UserCode = NameGenerator.GenerateUniqCode(),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "Defult.jpg"
            };

            _userRepository.AddUser(user);
            _userRepository.SaveChanges();

            return user.UserId;
        }

        public LoginServiceModel LoginUser(LoginUserViewModel login)
        {
            LoginServiceModel status = new LoginServiceModel();

            string password = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixText.FixEmail(login.Email);


            status.IsUserExist = _userRepository.IsEmailExist(email);

            if (status.IsUserExist)
            {
                Domain.Entities.User.User user = _userRepository.LoginUser(email, password);

                status.IsPasswordTrue = user != null ? true : false;
                status.IsActive = user != null ? user.IsActive : false;
            }

            return status;

        }

        public Enums.UserEnums.ActiveUserEnum SetActiveAccount(string userCode, bool activeStatus)
        {

            Domain.Entities.User.User user = _userRepository.GetUserFromActiveCode(userCode);

            if(user == null)
            {
                return Enums.UserEnums.ActiveUserEnum.CodeIsNotValid;
            }

            if (user.IsActive == activeStatus)
            {
                return Enums.UserEnums.ActiveUserEnum.CurrentStatus;
            }

            user.IsActive = true;
            user.UserCode = NameGenerator.GenerateUniqCode();

            _userRepository.SaveChanges();

            if (user.IsActive != activeStatus || user.UserCode == userCode)
            {
                return Enums.UserEnums.ActiveUserEnum.ServerError;
            }

            return Enums.UserEnums.ActiveUserEnum.Successful;
        }
    }
}
