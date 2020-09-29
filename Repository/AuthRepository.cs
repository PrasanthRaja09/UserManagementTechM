using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using System.Linq;

namespace Repository
{
    public class AuthRepository : RepositoryBase<Users>, IAuthRepository
    {
        public AuthRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public Users ValidateUser(Auth authUser)
        {
            return FindByCondition(user => user.UserName.Equals(authUser.UserName))
                .FirstOrDefault();
        }
    }
}
