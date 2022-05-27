using EHealth.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHealth.Services.Default
{
    public class DefaultDoctorsService : IDoctorsService
    {
        public async Task<DoctorModel> GetDoctorAsync(int id)
        {
            await Task.CompletedTask;
            return null;
        }

        public async Task<IEnumerable<DoctorModel>> GetDoctorsListAsync()
        {
            await Task.CompletedTask;
            return Array.Empty<DoctorModel>();
        }
    }
}
