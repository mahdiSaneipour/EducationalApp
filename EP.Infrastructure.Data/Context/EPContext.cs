using EP.Domain.Entities.Course;
using EP.Domain.Entities.Order;
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

        public DbSet<UserCourses> UserCourses { get; set; }

        public DbSet<UserDiscounts> UserDiscounts { get; set; }

        #endregion

        #region Wallet

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<WalletType> WalletTypes { get; set; }

        #endregion

        #region Permissions

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Courses

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseGroup> CourseGroupes { get; set; }

        public DbSet<CourseEpisode> Episodes { get; set; }

        public DbSet<CourseLevel> Levels { get; set; }

        public DbSet<CourseStatus> Statuses { get; set; }

        public DbSet<CourseComment> CourseComments { get; set; }

        public DbSet<CourseVote> CourseVotes { get; set; }

        #endregion

        #region Orders

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<CourseGroup>().HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Course>()
                .HasOne(x => x.CourseSubGroup)
                .WithMany(x => x.CoursesSubGroupes)
                .HasForeignKey(x => x.SubGroupId).OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserCourses>()
                .HasOne(us => us.User)
                .WithMany(us => us.UserCourses)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CourseComment>()
                .HasOne(cm => cm.User)
                .WithMany(cm => cm.CourseComments)
                .HasForeignKey(cm => cm.UserId).OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CourseVote>()
                .HasOne(cv => cv.User)
                .WithMany(cv => cv.Votes)
                .HasForeignKey(cv => cv.UserId).OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
