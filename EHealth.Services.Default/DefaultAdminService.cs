using EHealth.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public class DefaultAdminService : IAdminService
    {
        private readonly IUnitOfWork unitOfWork;

        public DefaultAdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddDoctor(string fullName)
        {
            await unitOfWork.DoctorRepository.Create(new() 
            {
                FullName = fullName
            });
            await unitOfWork.SaveChangesAsync();
        }

        public async Task AddDoctorToOccupation(int id, int occupationId)
        {
            var occupation = await unitOfWork.OccupationRepository.Get(occupationId);
            await unitOfWork.DoctorRepository.Update(id, d => 
            {
                d.Occupations.Add(occupation);
            });
            await unitOfWork.SaveChangesAsync();
        }

        public async Task AddOccupationAsync(string occupation)
        {
            await unitOfWork.OccupationRepository.Create(new()
            {
                Name = occupation
            });
            await unitOfWork.SaveChangesAsync();
        }

        public async Task AddStatusAsync(string status)
        {
            await unitOfWork.StatusRepository.Create(new()
            {
                Name = status
            });
            await unitOfWork.SaveChangesAsync();
        }
    }
}
