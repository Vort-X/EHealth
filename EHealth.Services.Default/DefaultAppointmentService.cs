using EHealth.DataAccess;
using EHealth.Entity;
using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public class DefaultAppointmentService : IAppointmentService
    {
        private const string pendingStatus = "Pending";

        private readonly IUnitOfWork unitOfWork;
        private int? pendingStatusId = null;

        public DefaultAppointmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAppointmentTimeToDoctorAsync(string fullName, DateTime dateTime)
        {
            var times = await unitOfWork.AppointmentTimeRepository.GetAll();
            var time = times.SingleOrDefault(t => t.AvailableTime == dateTime);
            if (time is null)
            {
                time = new()
                {
                    AvailableTime = dateTime
                };
                await unitOfWork.AppointmentTimeRepository.Create(time);
            }
            var doctor = (await unitOfWork.DoctorRepository.GetAll()).Single(d => d.FullName == fullName);
            await unitOfWork.DoctorRepository.Update(doctor.Id, d => 
            {
                d.AvailableAppointmentTime.Add(time);
            });
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CompleteAppointmentAsync(int id, int status, string diagnosis = null)
        {
            var update = unitOfWork.HistoryRepository.Update(id, h =>
            {
                h.StatusId = status;
                h.Diagnosis = diagnosis;
            });
            await update;
            await unitOfWork.SaveChangesAsync();
            return update.IsCompletedSuccessfully;
        }

        public async Task<IEnumerable<HistoryModel>> GetHistoryForDoctorAsync(string doctorFullName)
        {
            var entities = await unitOfWork.HistoryRepository.GetAll();
            return entities.Where(e => e.Doctor.FullName == doctorFullName).Select(ToModel);
        }

        public async Task<IEnumerable<HistoryModel>> GetHistoryForPatientAsync(string patientFullName)
        {
            var entities = await unitOfWork.HistoryRepository.GetAll();
            return entities.Where(e => e.PatientFullName == patientFullName).Select(ToModel);
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetStatuses()
        {
            var statuses = await unitOfWork.StatusRepository.GetAll();
            return statuses.ToDictionary(s => s.Id, s => s.Name);
        }

        public async Task<bool> ScheduleAppointmentAsync(string patientName, int doctorId, int appointmentTimeId)
        {
            if (pendingStatusId == null)
            {
                var statuses = await unitOfWork.StatusRepository.GetAll();
                pendingStatusId = statuses.Single(s => s.Name == pendingStatus).Id;
            }
            HistoryEntity history = new()
            {
                PatientFullName = patientName,
                DoctorId = doctorId,
                AppointmentTimeEntityId = appointmentTimeId,
                StatusId = pendingStatusId.Value
            };
            await unitOfWork.HistoryRepository.Create(history);
            await unitOfWork.DoctorRepository.Update(doctorId, d =>
            {
                var time = d.AvailableAppointmentTime.Find(t => t.Id == appointmentTimeId);
                d.AvailableAppointmentTime.Remove(time);
            });

            var save = unitOfWork.SaveChangesAsync();
            await save;
            return save.IsCompletedSuccessfully;
        }

        private static HistoryModel ToModel(HistoryEntity entity)
        {
            return new HistoryModel()
            {
                Id = entity.Id,
                PatientFullName = entity.PatientFullName,
                DoctorFullName = entity.Doctor.FullName,
                AppointmentDateTime = entity.AppointmentDateTime.AvailableTime,
                Status = entity.Status.Name,
                Diagnosis = entity.Diagnosis
            };
        }
    }
}
