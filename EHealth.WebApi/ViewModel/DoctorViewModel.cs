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
        public DateTime[] AvailableAppointmentTime { get; set; }

        public Dictionary<string, string[]> AvailableAppointmentTimeByDate => 
            AvailableAppointmentTime
                    .GroupBy(t => $"{t.DayOfYear}.{t.Month}", t => $"{t.Hour}:{t.Minute}")
                    .ToDictionary(g => g.Key, g => g.ToArray());
    }
}
