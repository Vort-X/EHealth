namespace EHealth.Model
{
    public class AppointmentModel
    {
        public string PatientFullName { get; set; }
        public int DoctorId { get; set; }
        public System.DateTime AppointmentDateTime { get; set; }
    }
}