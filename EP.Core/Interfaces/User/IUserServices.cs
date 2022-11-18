using EP.Core.DTOs.AccountViewModels;
using EP.Core.Enums.UserEnums;
using EP.Core.ServiceModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Interfaces.User
{
    public interface IUserServices
    {
        public bool IsUserNameExist(string Username);

        public bool IsEmailExist(string Email);

        public int AddUser(RegisterUserViewModel register);

        public LoginServiceModel LoginUser(LoginUserViewModel login);

        public Enums.UserEnums.ActiveUserEnum SetActiveAccount(string userCode, bool activeStatus);

        public ForgotPasswordEnum ForgotPassword(ForgotPasswordViewModel forgotPassword);

        public ResetPasswordEnums ResetPassword(ResetPasswordViewModel resetPassword);
    }
}
