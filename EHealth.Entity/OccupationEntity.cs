namespace EHealth.Entity
{
    public class OccupationEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DoctorEntity[] Doctors { get; set; }
    }
}
