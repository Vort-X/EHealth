using System;
using System.Threading.Tasks;

namespace EHealth.Services
{
    public interface IAdminService
    {
        Task AddDoctor(string fullName);
        Task AddDoctorToOccupation(int id, int occupationId);
        Task AddOccupationAsync(string occupation);
        Task AddStatusAsync(string status);
    }
}
