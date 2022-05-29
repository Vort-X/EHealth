using EHealth.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.DataAccess
{
    class DoctorRepository : DefaultRepository<DoctorEntity, int>
    {
        public DoctorRepository(EHealthDbContext context) : base(context)
        {
        }

        public override async Task<DoctorEntity> Get(int key)
        {
            return await context.Doctors
                .Include(d => d.AvailableAppointmentTime)
                .Include(d => d.Occupations)
                .FirstAsync(d => d.Id == key);
        }

        public override async Task<IEnumerable<DoctorEntity>> GetAll()
        {
            return await context.Doctors
                .Include(d => d.AvailableAppointmentTime)
                .Include(d => d.Occupations)
                .ToListAsync();
        }
    }
}
