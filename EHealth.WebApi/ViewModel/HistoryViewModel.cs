using System;

namespace EHealth.WebApi.ViewModel
{
    public class HistoryViewModel
    {
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string AppointmentDate => $"{AppointmentDateTime.DayOfYear}.{AppointmentDateTime.Month}";
        public string AppointmentTime => $"{AppointmentDateTime.Hour}:{AppointmentDateTime.Minute}";
        public string Status { get; set; }
        public string Diagnosis { get; set; }
    }
}
