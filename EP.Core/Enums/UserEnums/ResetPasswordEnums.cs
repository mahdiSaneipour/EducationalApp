using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Enums.UserEnums
{
    public enum ResetPasswordEnums
    {
        UserCodeIsNotValid,
        PasswordAndConfirmPasswordAreNotMatch,
        Successful,
        ServerError
    }
}
