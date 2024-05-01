using Cinema.DAL.Context;
using Cinema.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DAL.Implemantations
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context;
        public GenericRepository<ApplicationUser> ApplicationUserRepository { get; set; }
        public GenericRepository<Movie> MovieRepository { get; set; }
        public GenericRepository<Genre> GenreRepository { get; set; }
        public GenericRepository<Reservation> ReservationRepository { get; set; }
        public GenericRepository<SeatReservation> SeatResarvationRepository { get; set; }
        public GenericRepository<Session> SessionRepository { get; set; }
        public GenericRepository<CinemaRoom> CinemaRoomRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;

            ApplicationUserRepository = new GenericRepository<ApplicationUser>(context);
            MovieRepository = new GenericRepository<Movie>(context);
            GenreRepository = new GenericRepository<Genre>(context);
            ReservationRepository = new GenericRepository<Reservation>(context);
            SeatResarvationRepository = new GenericRepository<SeatReservation>(context);
            SessionRepository = new GenericRepository<Session>(context);
            CinemaRoomRepository = new GenericRepository<CinemaRoom>(context);
        }


        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
