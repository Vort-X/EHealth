using EHealth.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public interface IDoctorsService
    {
        Task<DoctorModel> GetDoctorAsync(int id);
        Task<IEnumerable<DoctorModel>> GetDoctorsListAsync();
    }
}
