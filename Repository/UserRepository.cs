using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Linq;

namespace Repository
{
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public Users GetUserByUserName(string userName)
        {
            return FindByCondition(a => a.UserName.Equals(userName)).FirstOrDefault();
        }

        public Users GetUserByUserID(int userID)
        {
            return FindByCondition(a => a.UserID.Equals(userID)).FirstOrDefault();
        }

        public int AddAdminUser(Users userDetails)
        {
            Create(userDetails);
            Save();
            return FindAll().OrderByDescending(a => a.UserID).Take(1).FirstOrDefault().UserID;
        }

        public int AddUser(Users userDetails)
        {
            Create(userDetails);
            Save();
            return FindAll().OrderByDescending(a => a.UserID).Take(1).FirstOrDefault().UserID;
        }

        public void DeleteUser(int userID)
        {
            var _tempUserDetails = FindByCondition(a => a.UserID.Equals(userID)).FirstOrDefault();
            Delete(_tempUserDetails);
            Save();
        }

        public void UpdateUser(Users userDetails)
        {
            Update(userDetails);
            Save();
        }
    }
}
