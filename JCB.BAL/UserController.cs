using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JCB.Entities;

using JCB.DAL;

namespace JCB.BAL
{
    public class UserController
    {
        UserManager db = new UserManager();

        public int UpdatePassword(Guid id, string password)
        {
            return db.UpdatePassword(id, password);
        }
        public User LoginUser(string username, string password)
        {
            return db.LoginUser(username, password);
        }
        public List<User> GetUsers(int branchId)
        {
            return db.GetUsers(branchId);

        }

        public User GetUserById(Guid userId)
        {
            return db.GetUserById(userId);

        }
        public List<User> GetUsers(Enumerations.UserType type, int branchId)
        {
            return db.GetUsers(type, branchId);

        }

        public int Insert(User item)
        {
            return db.Insert(item);
        }

        public int Update(User item)
        {
            return db.Update(item);
        }
    }
}
