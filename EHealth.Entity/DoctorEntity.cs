namespace EHealth.Entity
{
    public class DoctorEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public OccupationEntity[] Occupations { get; set; }
        public AppointmentTimeEntity[] AvailableAppointmentTime { get; set; }
    }
}
