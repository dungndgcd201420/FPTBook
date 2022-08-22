using FPTBook.Models;
using FPTBook.Utils;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace FPTBook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
              : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "b74ddd14-6340-4840-95c2-db12554843e5", Name = Role.CUSTOMER, ConcurrencyStamp = "1", NormalizedName = Role.CUSTOMER },
                new IdentityRole() { Id = "87az93ba-d201-2597-edc1-d211f91b7cb1", Name = Role.OWNER, ConcurrencyStamp = "2", NormalizedName = Role.OWNER },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = Role.ADMIN, ConcurrencyStamp = "3", NormalizedName = Role.ADMIN }
                );
        }
    }
}
