namespace EHealth.Entity
{
    public class HistoryEntity
    {
        public int Id { get; set; }
        public string PatientFullName { get; set; }
        public int DoctorId { get; set; }
        public DoctorEntity Doctor { get; set; }
        public int AppointmentTimeEntityId { get; set; }
        public AppointmentTimeEntity AppointmentDateTime { get; set; }
        public int StatusId { get; set; }
        public StatusEntity Status { get; set; }
        public string Diagnosis { get; set; }
    }
}
