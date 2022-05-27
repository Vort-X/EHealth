using EHealth.Model;
using EHealth.WebApi.ViewModel;

namespace EHealth.WebApi.Mappers
{
    public static class AppointmentMapper
    {
        public static AppointmentModel ToModel(this AppointmentViewModel viewModel, string patientFullName)
        {
            return new()
            { 
                PatientFullName = patientFullName,
                DoctorId = viewModel.DoctorId,
                AppointmentDateTime = viewModel.AppointmentDateTime
            };
        }
    }
}
