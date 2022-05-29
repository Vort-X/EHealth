using EHealth.Entity.Extentions;
using System;
using System.Collections.Generic;

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
        public List<DoctorEntity> AvailableDoctors { get; set; }
    }
}
