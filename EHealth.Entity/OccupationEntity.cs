using System.Collections.Generic;

namespace EHealth.Entity
{
    public class OccupationEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DoctorEntity> Doctors { get; set; }
    }
}
