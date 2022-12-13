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

        public int AddUser(Domain.Entities.User.User user);

        public Domain.Entities.User.User GetUserByEmail(string email);

        public Domain.Entities.User.User GetUserByUserId(int userId);

        public Entities.User.User LoginUser(string email, string password);

        public Domain.Entities.User.User GetUserFromActiveCode(string userCode);

        public int UpdateUser(Domain.Entities.User.User user);

        public bool CheckPassword(string password, int userId);

        public IQueryable<Domain.Entities.User.User> GetUsersForAdmin(string filterEmail, string filterUsername);

        public void SaveChanges();
    }
}
