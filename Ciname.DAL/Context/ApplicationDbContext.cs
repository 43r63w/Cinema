using Cinema.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace Cinema.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<CinemaRoom> CinemaRooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<SeatReserved> SeatReserved { get; set; }
        public DbSet<Session> Sessions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    Password = "123451",
                }
                );

            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    Id = 1,
                    Name = "Sci-fi",
                }
                );

            modelBuilder.Entity<Movie>().HasData(
               new Movie
               {
                   Id = 1,
                   Title = "Test movie",
                   GenreId = 1,
               }
               );



        }
    }
}
