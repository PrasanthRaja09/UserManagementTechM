using Entities.Models;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<Users>
    {
        int AddAdminUser(Users userDetails);
        Users GetUserByUserName(string userName);
        Users GetUserByUserID(int userID);
        int AddUser(Users userDetails);
        void UpdateUser(Users userDetails);
        void DeleteUser(int userID);
    }
}
