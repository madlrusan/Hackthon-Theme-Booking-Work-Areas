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

        public async Task<List<Office>> GetOffices(bool includeFloors)
        {
            var query = _dbContext.Set<Office>().AsNoTracking();

            if (includeFloors)
               query = query.Include(o => o.Floors);

            return await query.ToListAsync();
        }
    }
}
