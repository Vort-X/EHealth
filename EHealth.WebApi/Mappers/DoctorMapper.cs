using EHealth.Model;
using EHealth.WebApi.ViewModel;

namespace EHealth.WebApi.Mappers
{
    public static class DoctorMapper
    {
        public static DoctorViewModel ToViewModel(this DoctorModel model)
        {
            return new()
            {
                Id = model.Id,
                FullName = model.FullName,
                Occupations = model.Occupations,
                AvailableAppointmentTime = model.AvailableAppointmentTime,
            };
        }
    }
}