namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IAuthRepository AuthDetails { get; }
        IUserRepository UserDetails { get; }
        IUserRoleRepository UserRoleDetails { get; }
        IActivity ActivityDetails { get; }
        void Save();
    }
}
