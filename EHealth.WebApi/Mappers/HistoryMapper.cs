using EHealth.Model;
using EHealth.WebApi.ViewModel;

namespace EHealth.WebApi.Mappers
{
    public static class HistoryMapper
    {
        public static HistoryViewModel ToViewModel(this HistoryModel model)
        {
            return new()
            {
                PatientFullName = model.PatientFullName,
                DoctorFullName = model.DoctorFullName,
                AppointmentDateTime = model.AppointmentDateTime,
                Status = model.Status,
                Diagnosis = model.Diagnosis,
            };
        }
    }
}
