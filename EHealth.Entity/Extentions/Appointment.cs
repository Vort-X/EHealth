using System;

namespace EHealth.Entity.Extentions
{
    static class Appointment
    {
        public static DateTime AsSchedule(DateTime input)
        {
            return new(0, input.Month, input.Day, input.Hour, input.Minute, 0);
        }
    }
}
