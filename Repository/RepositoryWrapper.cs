using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IAuthRepository _authDetails;
        private IUserRepository _userDetails;
        private IActivity _activity;
        private IUserRoleRepository _userRoleDetails;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IAuthRepository AuthDetails
        {
            get
            {
                if (_authDetails == null)
                {
                    _authDetails = new AuthRepository(_repoContext);
                }

                return _authDetails;
            }
        }

        public IUserRepository UserDetails
        {
            get
            {
                if (_userDetails == null)
                {
                    _userDetails = new UserRepository(_repoContext);
                }

                return _userDetails;
            }
        }

        public IUserRoleRepository UserRoleDetails
        {
            get
            {
                if (_userRoleDetails == null)
                {
                    _userRoleDetails = new UserRoleRepository(_repoContext);
                }

                return _userRoleDetails;
            }
        }

        public IActivity ActivityDetails
        {
            get
            {
                if (_activity == null)
                {
                    _activity = new ActivityRepository(_repoContext);
                }

                return _activity;
            }
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
