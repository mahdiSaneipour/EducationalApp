using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Domain.Interfaces.User
{
    public interface IUserRepository
    {
        public bool IsUserNameExist(string username);

        public bool IsEmailExist(string email);
    }
}
