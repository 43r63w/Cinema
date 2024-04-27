using Cinema.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<User>? userRepository;
        // TODO: add more generic repositories right here

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
