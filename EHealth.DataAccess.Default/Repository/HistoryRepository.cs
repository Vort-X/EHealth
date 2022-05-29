using EHealth.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.DataAccess
{
    class HistoryRepository : DefaultRepository<HistoryEntity, int>
    {
        public HistoryRepository(EHealthDbContext context) : base(context)
        {
        }

        public override async Task<HistoryEntity> Get(int key)
        {
            return await context.Histories
                .Include(h => h.AppointmentDateTime)
                .Include(h => h.Status)
                .Include(h => h.Doctor)
                .FirstAsync(h => h.Id == key);
        }

        public override async Task<IEnumerable<HistoryEntity>> GetAll()
        {
            return await context.Histories
                .Include(h => h.AppointmentDateTime)
                .Include(h => h.Status)
                .Include(h => h.Doctor)
                .ToListAsync();
        }
    }
}
