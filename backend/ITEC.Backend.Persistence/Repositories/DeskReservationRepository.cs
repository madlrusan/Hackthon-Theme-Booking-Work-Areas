using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Database;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ITEC.Backend.Persistence.Repositories
{
    public class DeskReservationRepository : Repository<DeskReservation>, IRepository<DeskReservation>, IDeskReservationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DeskReservationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DeskReservation>> GetFutureReservationsForDesk(int deskId)
        {
            var tomorrowDate = DateTime.Now.AddDays(1);
            return await _dbContext.Set<DeskReservation>().Where(p => p.DeskId == deskId && p.ReservationDate >= new DateTime(tomorrowDate.Year, tomorrowDate.Month, tomorrowDate.Day)).ToListAsync();
        }

        public async Task<List<DeskReservation>> GetFutureReservationsForUser(string userId)
        {
            var tomorrowDate = DateTime.Now.AddDays(1);
            return await _dbContext.Set<DeskReservation>().OrderBy(p => p.ReservationDate).Where(p => p.CreatedByUserId == userId && p.ReservationDate >= new DateTime(tomorrowDate.Year, tomorrowDate.Month, tomorrowDate.Day)).ToListAsync();
        }
    }
}
