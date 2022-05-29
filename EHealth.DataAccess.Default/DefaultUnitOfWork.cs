using EHealth.Entity;
using System;
using System.Threading.Tasks;

namespace EHealth.DataAccess
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly EHealthDbContext context;

        public DefaultUnitOfWork(EHealthDbContext context)
        {
            this.context = context;
        }

        public IRepository<AppointmentTimeEntity, int> AppointmentTimeRepository => new DefaultRepository<AppointmentTimeEntity, int>(context);

        public IRepository<DoctorEntity, int> DoctorRepository => new DefaultRepository<DoctorEntity, int>(context);

        public IRepository<HistoryEntity, int> HistoryRepository => new DefaultRepository<HistoryEntity, int>(context);

        public IRepository<OccupationEntity, int> OccupationRepository => new DefaultRepository<OccupationEntity, int>(context);

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DefaultUnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
