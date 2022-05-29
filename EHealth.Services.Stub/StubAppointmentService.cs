using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public class StubAppointmentService : IAppointmentService
    {
        public Task AddAppointmentTimeToDoctorAsync(string doctorFullName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CompleteAppointmentAsync(int id, int status, string diagnosis = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HistoryModel>> GetHistoryForDoctorAsync(string doctorFullName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HistoryModel>> GetHistoryForPatientAsync(string patientFullName)
        {
            return await Task.FromResult(new List<HistoryModel>()
            {
                new()
                {
                    PatientFullName = "Mihailo P",
                    DoctorFullName = "Robert D",
                    AppointmentDateTime = new DateTime(2022, 06, 01, 17, 00, 00),
                    Status = "Completed",
                    Diagnosis = "Sleep deprivation",
                },
                new()
                {
                    PatientFullName = "Mihailo P",
                    DoctorFullName = "Halyatin V",
                    AppointmentDateTime = new DateTime(2022, 06, 20, 15, 00, 00),
                    Status = "Pending",
                }
            });
        }

        public Task<IEnumerable<KeyValuePair<int, string>>> GetStatuses()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ScheduleAppointmentAsync(AppointmentModel appointmentModel)
        {
            return await Task.FromResult(true);
        }

        public Task<bool> ScheduleAppointmentAsync(string patientName, int doctorId, int appointmentTime)
        {
            throw new NotImplementedException();
        }
    }
}
