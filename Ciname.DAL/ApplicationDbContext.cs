using Cinema.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cinema.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> users { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add initial data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "cinema_admin@gmail.com",
                    Password = "password",
                },
                new User
                {
                    Id = 2,
                    Email = "cinema_user1@gmail.com",
                    Password = "password",
                }
            );
        }

    }
}
