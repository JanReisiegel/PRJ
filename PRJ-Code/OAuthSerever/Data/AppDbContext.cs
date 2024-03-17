﻿using Microsoft.EntityFrameworkCore;
using OAuthSerever.Models;

namespace OAuthSerever.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        /*public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x => x.Id);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = "ADMIN",
                Name = "admin",
                Surname = "admin",
                Email = "admin",
                BirthDate = DateOnly.FromDateTime(DateTime.Now),
                PersonIdentityNumber = "",
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = "ADMIN",
                Name = "Admin"
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                UserId = "ADMIN",
                RoleId = "ADMIN",
            });
        }*/
    }
}
