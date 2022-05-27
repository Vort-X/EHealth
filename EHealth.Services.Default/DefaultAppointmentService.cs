using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.Services.Default
{
    public class DefaultAppointmentService : IAppointmentService
    {
        public async Task<IEnumerable<HistoryModel>> GetHistoryForPatientAsync(string patientFullName)
        {
            await Task.CompletedTask;
            return Array.Empty<HistoryModel>();
        }

        public async Task<bool> ScheduleAppointmentAsync(AppointmentModel appointmentModel)
        {
            await Task.CompletedTask;
            return false;
        }
    }
}
