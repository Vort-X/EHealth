using System;
using System.Collections.Generic;
using System.Linq;

namespace EHealth.WebApi.ViewModel
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string[] Occupations { get; set; }
        public IEnumerable<KeyValuePair<int, DateTime>> AvailableAppointmentTime { get; set; }
    }
}
