namespace EHealth.Model
{
    public class HistoryModel
    {
        public int Id { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public System.DateTime AppointmentDateTime { get; set; }
        public string Status { get; set; }
        public string Diagnosis { get; set; }
    }
}