using EHealth.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<HistoryModel>> GetHistoryForPatientAsync(string patientFullName);
        Task<bool> ScheduleAppointmentAsync(AppointmentModel appointmentModel);
    }
}
