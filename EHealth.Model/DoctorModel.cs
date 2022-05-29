using System;
using System.Collections.Generic;

namespace EHealth.Model
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string[] Occupations { get; set; }
        public IEnumerable<KeyValuePair<int, DateTime>> AvailableAppointmentTime { get; set; }
    }
}