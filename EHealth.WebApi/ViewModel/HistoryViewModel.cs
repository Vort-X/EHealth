using System;

namespace EHealth.WebApi.ViewModel
{
    public class HistoryViewModel
    {
        public int Id { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        //public string AppointmentDate => $"{AppointmentDateTime.Day}.{AppointmentDateTime.Month}";
        //public string AppointmentTime => $"{AppointmentDateTime.Hour}:{AppointmentDateTime.Minute}";
        public string Status { get; set; }
        public string Diagnosis { get; set; }
    }
}
