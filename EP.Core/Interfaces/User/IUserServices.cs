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
    }
}
