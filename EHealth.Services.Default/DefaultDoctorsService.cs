using EHealth.DataAccess;
using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealth.Services.Default
{
    public class DefaultDoctorsService : IDoctorsService
    {
        private readonly IUnitOfWork unitOfWork;

        public DefaultDoctorsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<DoctorModel> GetDoctorAsync(int id)
        {
            var entity = await unitOfWork.DoctorRepository.Get(id);
            return entity is null ? new() : new()
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Occupations = entity.Occupations.Select(o => o.Name).ToArray(),
                AvailableAppointmentTime = entity.AvailableAppointmentTime.Select(t => t.AvailableTime).ToArray()
            };
        }

        public async Task<IEnumerable<DoctorModel>> GetDoctorsListAsync()
        {
            var entities = await unitOfWork.DoctorRepository.GetAll();
            await Task.CompletedTask;
            return entities.Select(entity => new DoctorModel() 
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Occupations = entity.Occupations.Select(o => o.Name).ToArray(),
                AvailableAppointmentTime = entity.AvailableAppointmentTime.Select(t => t.AvailableTime).ToArray()
            });
        }
    }
}
