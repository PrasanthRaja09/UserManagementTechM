using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Linq;

namespace Repository
{
    public class UserRoleRepository : RepositoryBase<User_Roles>, IUserRoleRepository
    {
        public UserRoleRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public User_Roles GetRoleByUserID(int userID)
        {
            return FindByCondition(a => a.UserID.Equals(userID)).FirstOrDefault();
        }

        public void AddUserRoles(User_Roles roleDetails)
        {
            Create(roleDetails);
            Save();
        }

        public void UpdateUserRoles(User_Roles roleDetails)
        {
            Update(roleDetails);
            Save();
        }

        public void DeleteUserRoles(int userID)
        {
            var _tempUserDetails = FindByCondition(a => a.UserID.Equals(userID)).FirstOrDefault();
            Delete(_tempUserDetails);
            Save();
        }
    }
}
