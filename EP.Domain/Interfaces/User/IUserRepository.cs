using EP.Domain.Entities.User;
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

        public int AddUser(EP.Domain.Entities.User.User user);

        public Entities.User.User LoginUser(string email, string password);

        public void SaveChanges();
    }
}
