using System.Collections.Generic;

namespace EHealth.Entity
{
    public class DoctorEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<OccupationEntity> Occupations { get; set; }
        public List<AppointmentTimeEntity> AvailableAppointmentTime { get; set; }
    }
}
