using Entities.Model;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options)
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermissions>()
                .HasKey(o => new { o.id, o.rid });
            modelBuilder.Entity<Audit_logs>()
             .HasKey(o => new { o.uid, o.id });
            modelBuilder.Entity<Roles>()
            .HasKey(o => new { o.id});
        }

        public DbSet<Users> UserLogin { get; set; }
        public DbSet<User_Roles> UserRoles { get; set; }
        public DbSet<RolePermissions> RolePermission { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
