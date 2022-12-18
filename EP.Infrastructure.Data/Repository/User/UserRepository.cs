using EP.Domain.Entities.User;
using EP.Domain.Interfaces.User;
using EP.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly EPContext _context;

        public UserRepository(EPContext context)
        {
            _context = context;
        }

        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsUserNameExist(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

        public int AddUser(EP.Domain.Entities.User.User user)
        {
            _context.Users.Add(user);
            return user.UserId;
        }

        public EP.Domain.Entities.User.User LoginUser(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public Domain.Entities.User.User GetUserFromActiveCode(string userCode)
        {
            return _context.Users.FirstOrDefault(u => u.UserCode == userCode);
        }

        public Domain.Entities.User.User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public Domain.Entities.User.User GetUserByUserId(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public bool CheckPassword(string password, int userId)
        {
            return !_context.Users.Where(u => u.UserId == userId && u.Password == password).Any();
        }

        public int UpdateUser(Domain.Entities.User.User user)
        {
            _context.Update(user);
            return user.UserId;
        }

        public IQueryable<Domain.Entities.User.User> GetUsersForAdmin(string filterEmail, string filterUsername, bool isDelete)
        {
            IQueryable<Domain.Entities.User.User> users = null;
            if (isDelete)
            {
                users = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete);
            }
            else
            {
                users = _context.Users;
            }

            if (!string.IsNullOrEmpty(filterEmail))
            {
                users = users.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUsername))
            {
                users = users.Where(u => u.UserName.Contains(filterUsername));
            }

            return users;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}