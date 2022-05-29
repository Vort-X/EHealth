using EHealth.Entity;
using System;
using System.Threading.Tasks;

namespace EHealth.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AppointmentTimeEntity, int> AppointmentTimeRepository { get; }
        IRepository<DoctorEntity, int> DoctorRepository { get; }
        IRepository<HistoryEntity, int> HistoryRepository { get; }
        IRepository<OccupationEntity, int> OccupationRepository { get; }
        IRepository<StatusEntity, int> StatusRepository { get; }

        Task SaveChangesAsync();
    }
}
