using EHealth.Entity.Extentions;
using System;

namespace EHealth.Entity
{
    public class AppointmentTimeEntity
    {
        private DateTime availableTime;

        public int Id { get; set; }
        public DateTime AvailableTime 
        { 
            get => availableTime; 
            set => availableTime = Appointment.AsSchedule(value); 
        }
        public DoctorEntity[] AvailableDoctors { get; set; }
        public HistoryEntity[] ScheduledAppointsment { get; set; }
    }
}
