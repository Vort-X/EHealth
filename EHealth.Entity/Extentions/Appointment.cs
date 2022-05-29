using System;

namespace EHealth.Entity.Extentions
{
    static class Appointment
    {
        public static DateTime AsSchedule(DateTime input)
        {
            return new(input.Year, input.Month, input.Day, input.Hour, input.Minute, 0);
        }
    }
}
