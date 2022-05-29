using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public interface IAppointmentService
    {
        Task AddAppointmentTimeToDoctorAsync(string doctorFullName, DateTime dateTime);
        Task<bool> CompleteAppointmentAsync(int id, int status, string diagnosis = null);
        Task<IEnumerable<HistoryModel>> GetHistoryForDoctorAsync(string doctorFullName);
        Task<IEnumerable<HistoryModel>> GetHistoryForPatientAsync(string patientFullName);
        Task<IEnumerable<KeyValuePair<int, string>>> GetStatuses();
        Task<bool> ScheduleAppointmentAsync(string patientName, int doctorId, int appointmentTime);
    }
}
