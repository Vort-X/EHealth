using EHealth.DataAccess;
using EHealth.Entity;
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
        private readonly IUnitOfWork unitOfWork;

        public DefaultAppointmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<HistoryModel>> GetHistoryForPatientAsync(string patientFullName)
        {
            var entities = await unitOfWork.HistoryRepository.GetAll();
            return entities.Where(e => e.PatientFullName == patientFullName)
                .Select(entity => new HistoryModel() 
                { 
                    PatientFullName = entity.PatientFullName,
                    DoctorFullName = entity.Doctor.FullName,
                    AppointmentDateTime = entity.AppointmentDateTime.AvailableTime,
                    Status = entity.Status,
                    Diagnosis = entity.Diagnosis
                });
        }

        public async Task<bool> ScheduleAppointmentAsync(AppointmentModel appointmentModel)
        {
            HistoryEntity model = new()
            {
                PatientFullName = appointmentModel.PatientFullName,
                DoctorId = appointmentModel.DoctorId,
                Status = "Pending"
            };
            var creation = unitOfWork.HistoryRepository.Create(model);
            await creation;
            return creation.IsCompletedSuccessfully;
        }
    }
}
