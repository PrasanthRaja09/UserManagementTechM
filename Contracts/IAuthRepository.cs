using Entities.DTO;
using Entities.Models;

namespace Contracts
{
    public interface IAuthRepository : IRepositoryBase<Users>
    {
        Users ValidateUser(Auth authUser);
    }
}
