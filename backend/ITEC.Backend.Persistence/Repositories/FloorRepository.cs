using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Database;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Persistence.Repositories
{
    public class FloorRepository : Repository<Floor>, IRepository<Floor>, IFloorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FloorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Floor> GetFloorWithDesksById(int floorId)
        {
            var tomorrowTime = DateTime.Now.AddDays(1);
            var reservations = await _dbContext.Set<DeskReservation>().Where(o => o.ReservationDate == new DateTime(tomorrowTime.Year, tomorrowTime.Month, tomorrowTime.Day)).ToListAsync();
            var floor = await _dbContext.Set<Floor>().AsNoTracking().Include(o => o.Desks).FirstOrDefaultAsync(o => o.Id == floorId);
            foreach (var desk in floor.Desks)
                desk.Reserved = reservations.Any(p => p.DeskId == desk.Id);

            return floor;
        }
    }
}
