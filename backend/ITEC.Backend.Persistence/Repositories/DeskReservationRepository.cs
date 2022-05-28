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
            return await _dbContext.Set<DeskReservation>().Where(p => p.DeskId == deskId && p.ReservationDate >= DateTime.Now).ToListAsync();
        }
    }
}
