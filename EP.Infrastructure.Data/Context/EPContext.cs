﻿using EP.Domain.Entities.User;
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

    }
}