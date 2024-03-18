using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;
        public IProviderRepository Providers { get; }
        public IClientRepository Clients { get; }
        public IAppointmentSlotRepository Appointments { get; }
        public IReservationRepository Reservations { get; }

        

        public UnitOfWork(DbContextClass dbContext,
                            IProviderRepository providerRepository,
                            IClientRepository clientRepository,
                            IAppointmentSlotRepository appointmentSlotRepository,
                            IReservationRepository reservationRepository)
        {
            _dbContext = dbContext;
            Providers = providerRepository;
            Clients = clientRepository;
            Appointments = appointmentSlotRepository;
            Reservations = reservationRepository;
        }

        public async Task SaveAsync()
        {
            Console.WriteLine($"Starting SaveAsync on thread {Thread.CurrentThread.ManagedThreadId}");

            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
