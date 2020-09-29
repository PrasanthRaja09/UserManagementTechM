using Entities.Models;

namespace Contracts
{
    public interface IUserRoleRepository : IRepositoryBase<User_Roles>
    {
        User_Roles GetRoleByUserID(int userID);
        void AddUserRoles(User_Roles roleDetails);
        void UpdateUserRoles(User_Roles roleDetails);
        void DeleteUserRoles(int userID);
    }
}
