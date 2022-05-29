using EHealth.DataAccess;
using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealth.Services
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
            return entity is null ? null : ToModel(entity);
        }

        public async Task<IEnumerable<DoctorModel>> GetDoctorsListAsync()
        {
            var entities = await unitOfWork.DoctorRepository.GetAll();
            return entities.Select(ToModel);
        }

        //TODO: Inject mappers
        private static DoctorModel ToModel(Entity.DoctorEntity entity)
        {
            return new DoctorModel()
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Occupations = entity.Occupations.Select(o => o.Name).ToArray(),
                AvailableAppointmentTime = entity.AvailableAppointmentTime.ToDictionary(t => t.Id, t => t.AvailableTime)
            };
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> GetOccupations()
        {
            return (await unitOfWork.OccupationRepository.GetAll()).ToDictionary(o => o.Id, o => o.Name);
        }
    }
}
