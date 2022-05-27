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
    public class OfficeRepository : IOfficeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OfficeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOffice(Office office)
        {
            await _dbContext.Set<Office>().AddAsync(office);
            await _dbContext.SaveChangesAsync();
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
