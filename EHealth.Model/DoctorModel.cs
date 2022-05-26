using System;

namespace EHealth.Model
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string[] Occupations { get; set; }
        public DateTime[] AvailableAppointmentTime { get; set; }
    }
}