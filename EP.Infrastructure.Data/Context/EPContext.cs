using EP.Domain.Entities.Course;
using EP.Domain.Entities.Permission;
using EP.Domain.Entities.User;
using EP.Domain.Entities.Wallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Infrastructure.Data.Context
{
    public class EPContext : DbContext
    {

        public EPContext(DbContextOptions<EPContext> options):base(options)
        {

        }

        #region User

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Wallet

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<WalletType> WalletTypes { get; set; }

        #endregion

        #region Permissions

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region

        public DbSet<CourseGroupe> CourseGroupes { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);

            base.OnModelCreating(modelBuilder);
        }
    }
}
