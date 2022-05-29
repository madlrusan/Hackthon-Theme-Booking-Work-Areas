using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Database;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ITEC.Backend.Persistence.Repositories
{
    public class OfficeRepository : Repository<Office>, IOfficeRepository, IRepository<Office>
    {
        private readonly ApplicationDbContext _dbContext;

        public OfficeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Office> GetOfficeByIdWithDesks(int officeId)
        {
            var tomorrowTime = DateTime.Now.AddDays(1);
            var reservations = await _dbContext.Set<DeskReservation>().Where(o => o.ReservationDate == new DateTime(tomorrowTime.Year, tomorrowTime.Month, tomorrowTime.Day)).ToListAsync();
            var office = await _dbContext.Set<Office>().AsNoTracking().Include(o => o.Floors).ThenInclude(o => o.Desks).FirstOrDefaultAsync(o => o.Id == officeId);
            foreach(var floor in office.Floors)
            {
                foreach(var desk in floor.Desks)
                    desk.Reserved = reservations.Any(p => p.DeskId == desk.Id);
            }

            return office;
        }

        public async Task<List<Office>> GetOffices(bool includeFloors)
        {
            var query = _dbContext.Set<Office>().AsNoTracking();

            if (includeFloors)
               query = query.Include(o => o.Floors);

            return await query.ToListAsync();
        }
    }
}
