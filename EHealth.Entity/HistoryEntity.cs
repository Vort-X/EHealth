namespace EHealth.Entity
{
    public class HistoryEntity
    {
        public int Id { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public int AppointmentTimeEntityId { get; set; }
        public AppointmentTimeEntity AppointmentDateTime { get; set; }
        public string Status { get; set; }
        public string Diagnosis { get; set; }
    }
}
