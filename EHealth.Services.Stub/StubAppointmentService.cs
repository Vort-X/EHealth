using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public class StubAppointmentService : IAppointmentService
    {
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

        public async Task<bool> ScheduleAppointmentAsync(AppointmentModel appointmentModel)
        {
            return await Task.FromResult(true);
        }
    }
}
